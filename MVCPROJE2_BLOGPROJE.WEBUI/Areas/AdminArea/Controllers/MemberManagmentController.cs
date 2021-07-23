using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class MemberManagmentController : Controller
    {
        private readonly IUyeRepository _repository;
        private readonly IImageService _imageService;
        public MemberManagmentController(IUyeRepository repository,IImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            return View(_repository.Uyeler);
        }
        [HttpGet]
        public IActionResult Update(int id) => View(_repository.GetById(id));
        [HttpPost]
        public async Task<IActionResult> Update(Uye uye)
        {
            _repository.UyeGuncelle(uye);
            await _imageService.ImageRecordAsync(uye);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _repository.UyeSil(id);
            return RedirectToAction("Index");
        }
    }
}
