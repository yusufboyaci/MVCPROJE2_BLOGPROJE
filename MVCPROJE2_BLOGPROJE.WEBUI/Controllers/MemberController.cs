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
        public MemberController(IUyeRepository repository, IImageService imageService, IRegistrationService registrationService, IEmailSender emailSender)
        {
            _repository = repository;
            _imageService = imageService;
            _registrationService = registrationService;
            _emailSender = emailSender;
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
            return RedirectToAction("Index","MemberManagment", new { area = "AdminArea" });
        }

        [HttpGet]
        public IActionResult Delete(Uye uye) => View(uye);
        [HttpPost]
        public IActionResult Delete()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            _repository.UyeSil(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update(int id) => View(_repository.GetById(id));
        [HttpPost]
        public async Task<IActionResult> Update(Uye uye)
        {
            uye.ID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            uye.IsActive = true;
            await _imageService.ImageRecordAsync(uye);
            _repository.UyeGuncelle(uye);
            return RedirectToAction("Index", "Home");
        }

    }
}
