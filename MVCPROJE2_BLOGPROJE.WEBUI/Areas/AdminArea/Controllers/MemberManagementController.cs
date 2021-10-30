using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles ="Admin")]
    public class MemberManagementController : Controller
    {
        private readonly IUyeRepository _repository;
        private readonly IImageService _imageService;
        private readonly IUpdateRegistrationService _updateRegistrationService;
        private readonly UserManager<IdentityUser> _userManager;
        public MemberManagementController(IUyeRepository repository, IImageService imageService, IUpdateRegistrationService updateRegistrationService, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _imageService = imageService;
            _updateRegistrationService = updateRegistrationService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_repository.Uyeler);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id) => View(await _repository.GetByIdAsync(id));
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Uye uye)
        {
            if (ModelState.IsValid)
            {               

                await _imageService.ImageRecordAsync(uye);
               
                await _repository.UyeGuncelleAsync(uye);
            
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            Uye uye = await _repository.GetByIdAsync(id);
            IdentityUser user = _userManager.Users.FirstOrDefault(x => x.Email == uye.MailAdresi);
            await _repository.UyeSilAsync(id);
            await _updateRegistrationService.RemoveAsync(user.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id) => View(await _repository.GetByIdAsync(id));
    }
}
