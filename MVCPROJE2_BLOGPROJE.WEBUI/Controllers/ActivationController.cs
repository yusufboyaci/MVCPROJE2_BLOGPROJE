using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class ActivationController : Controller
    {
        private readonly IdentityUser _user;
        private readonly IUyeRepository _uyeRepository;
        public ActivationController(IdentityUser user, IUyeRepository uyeRepository)
        {
            _user = user;
            _uyeRepository = uyeRepository;
        }
        [HttpGet]
        public IActionResult Activation()
        {
            Uye uye = _uyeRepository.Uyeler.FirstOrDefault(x => x.MailAdresi == _user.Email);
            HttpContext.Session.SetInt32("id", uye.ID);
            return RedirectToAction("Index", "Home");
        }
    }
}
