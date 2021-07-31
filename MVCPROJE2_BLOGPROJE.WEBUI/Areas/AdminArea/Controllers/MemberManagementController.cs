using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class MemberManagementController : Controller
    {
        private readonly IUyeRepository _repository;
        private readonly IImageService _imageService;
        private readonly IUpdateRegistrationService _updateRegistrationService;
        public MemberManagementController(IUyeRepository repository, IImageService imageService, IUpdateRegistrationService updateRegistrationService)
        {
            _repository = repository;
            _imageService = imageService;
            _updateRegistrationService = updateRegistrationService;
        }
        public IActionResult Index()
        {
            return View(_repository.Uyeler);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id) => View(await _repository.GetByIdAsync(id));
        [HttpPost]
        public async Task<IActionResult> Update(Uye uye)
        {
            _repository.UyeGuncelle(uye);
            await _imageService.ImageRecordAsync(uye);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.UyeSilAsync(id);
            //_updateRegistrationService.RemoveAsync();
            return RedirectToAction("Index");
        }
    }
}
