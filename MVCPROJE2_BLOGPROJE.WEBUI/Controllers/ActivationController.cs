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
       
        private readonly IUyeRepository _uyeRepository;
        public ActivationController(IUyeRepository uyeRepository)
        {
           
            _uyeRepository = uyeRepository;
        }
        [HttpGet]
        public IActionResult Activation()
        {
          Uye uye=_uyeRepository.Uyeler.FirstOrDefault(x => x.MailAdresi == (string)TempData["uyeMail"]);
            uye.IsActive = true;
            return View();
        }
    }
}
