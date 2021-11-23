using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.CORE.ViewModels;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUyeRepository _repository;
        private readonly IImageService _imageService;
        private readonly IRegistrationService _registrationService;
        private readonly IEmailSender _emailSender;
        private readonly IUpdateRegistrationService _updateRegistrationService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberController(IUyeRepository repository, IImageService imageService, IRegistrationService registrationService, IEmailSender emailSender, IUpdateRegistrationService updateRegistrationService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _imageService = imageService;
            _registrationService = registrationService;
            _emailSender = emailSender;
            _updateRegistrationService = updateRegistrationService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        Random rnd = new Random();

        [HttpGet]
        public IActionResult Add() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Uye uye)
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel model = new RegisterViewModel
                {
                    Email = uye.MailAdresi,
                    Password = rnd.Next(1, 20).ToString() + Convert.ToChar(rnd.Next(65, 91)) + rnd.Next(10, 100).ToString() + Convert.ToChar(rnd.Next(97, 123))
                };

                model.ConfirmPassword = model.Password;

                IdentityResult result = await _registrationService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    await _imageService.ImageRecordAsync(uye);                  
                    await _repository.UyeEkleAsync(uye);
                    await _emailSender.SendEmailAsync(uye.MailAdresi, "Üyelik Şifreniz", $"Üyelik şifreniz: {model.Password}");
                    return RedirectToAction("Index", "MemberManagement", new { area = "AdminArea" });
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi");
            }
            return View(uye);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            await _repository.UyeSilAsync(id);
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(HttpContext.Session.GetString("accountId")));
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> MemberInfo() => View(await _repository.GetByIdAsync((int)HttpContext.Session.GetInt32("id")));


        [HttpGet]
        public async Task<IActionResult> Update() => View(await _repository.GetByIdAsync((int)HttpContext.Session.GetInt32("id")));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Uye uye, string currentPassword, string newPassword)
        {
            if (ModelState.IsValid)
            {
                uye.ID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                string identityUserId = HttpContext.Session.GetString("accountId");
                uye.IsActive = true;
                await _imageService.ImageRecordAsync(uye);
                await _repository.UyeGuncelleAsync(uye);               
                await _updateRegistrationService.UpdateAsync(identityUserId,uye.MailAdresi);
                if (currentPassword == null || newPassword == null)
                {                   
                    ViewBag.NullPasswordError = "Lütfen şifre alanını boş bırakmayınız";
                    return View();
                }
                else
                {
                    if (currentPassword != newPassword)
                    {
                        await _updateRegistrationService.UpdatePasswordAsync(identityUserId, currentPassword, newPassword);
                    }
                    else
                    {
                        ViewBag.PasswordError = "Lütfen kullandığınız şifre ile yeni şifreyi aynı yapmayınız!";
                        return View();
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}
