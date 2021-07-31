using Microsoft.AspNetCore.Http;
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

        public MemberController(IUyeRepository repository, IImageService imageService, IRegistrationService registrationService, IEmailSender emailSender, IUpdateRegistrationService updateRegistrationService)
        {
            _repository = repository;
            _imageService = imageService;
            _registrationService = registrationService;
            _emailSender = emailSender;
            _updateRegistrationService = updateRegistrationService;
        }
        Random rnd = new Random();

        [HttpGet]
        public IActionResult Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(Uye uye)
        {
            await _imageService.ImageRecordAsync(uye);
            await _repository.UyeEkleAsync(uye);
            RegisterViewModel model = new RegisterViewModel
            {
                Email = uye.MailAdresi,
                Password = rnd.Next(1, 20).ToString() + Convert.ToChar(rnd.Next(65, 91)) + rnd.Next(10, 100).ToString() + Convert.ToChar(rnd.Next(97, 123))
            };

            model.ConfirmPassword = model.Password;

            await _registrationService.RegisterAsync(model);
            await _emailSender.SendEmailAsync(uye.MailAdresi, "Üyelik Şifreniz", $"Üyelik şifreniz: {model.Password}");
            return RedirectToAction("Index", "MemberManagement", new { area = "AdminArea" });
        }

        [HttpGet]
        public IActionResult Delete(Uye uye) => View(uye);
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            await _repository.UyeSilAsync(id);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> MemberInfo() => View(await _repository.GetByIdAsync((int)HttpContext.Session.GetInt32("id")));


        [HttpGet]
        public async Task<IActionResult> Update() => View(Tuple.Create<Uye, RegisterViewModel>(await _repository.GetByIdAsync((int)HttpContext.Session.GetInt32("id")), new RegisterViewModel()));

        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "item1")] Uye uye, [Bind(Prefix = "item2")] RegisterViewModel model, string currentPassword, string newPassword)
        {
            uye.ID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            string identityUserId = HttpContext.Session.GetString("accountId");
            uye.IsActive = true;
            await _imageService.ImageRecordAsync(uye);
            _repository.UyeGuncelle(uye);
            model.Email = uye.MailAdresi;
            await _updateRegistrationService.UpdatePasswordAsync(identityUserId, currentPassword, newPassword);
            return RedirectToAction("Index", "Home");
        }

    }
}
