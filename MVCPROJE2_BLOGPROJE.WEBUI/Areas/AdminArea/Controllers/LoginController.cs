using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Areas.AdminArea.Controllers
{
    /*Bu controller iptal edilmiştir kullanılmamaktadır*/
    [Area("AdminArea")]
    public class LoginController : Controller
    {
        public IActionResult LoginForm()
        {
            return View();
        }
       [Route("AdminArea")]
        [HttpPost]
        public IActionResult AdminLogin(string userName, string password)
        {
            if (userName != null && password != null && userName.Equals("admin") && password.Equals("123"))
            {
                HttpContext.Session.SetString("username", userName);
                return RedirectToAction("Index","MemberManagement",new { area = "AdminArea" });
            }
            else
            {
                ViewBag.error = "Hatalı giriş";
                return View("LoginForm");
            }
        }
        [Route("AdminArea/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
