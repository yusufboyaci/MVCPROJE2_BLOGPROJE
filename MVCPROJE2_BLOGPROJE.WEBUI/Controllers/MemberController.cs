using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
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
       
        public MemberController(IUyeRepository repository,IImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }
        [HttpGet]
        public IActionResult Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(Uye uye)
        {
           await _imageService.ImageRecordAsync(uye);
           await _repository.UyeEkleAsync(uye);
            return View(uye);
        }

        [HttpGet]
        public IActionResult Delete(Uye uye) => View(uye);
        [HttpPost]
        public IActionResult Delete(int id)
        {
            id = (int) HttpContext.Session.GetInt32("id");
            _repository.UyeSil(id);
            return RedirectToAction("Index", "Home");
        }


    }
}
