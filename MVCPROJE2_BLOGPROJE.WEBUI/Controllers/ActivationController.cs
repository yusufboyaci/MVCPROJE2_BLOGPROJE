using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class ActivationController : Controller
    {
        [HttpGet]
        public IActionResult Activation() => RedirectToAction("Index","Home");
       

       
    }
}
