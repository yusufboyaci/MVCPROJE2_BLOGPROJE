using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.ViewModels;
using MVCPROJE2_BLOGPROJE.SERVICES.EmailService.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    Guid activationCode = Guid.NewGuid();
                    await _emailSender.SendEmailAsync(user.Email, "Aktivasyon Maili", "<br /><a href = '" + string.Format("https://localhost:44318/Activation/Activation/{0}", activationCode) + "'>Üye olmak için tıklayınız</a>");
                  
                    return RedirectToAction("Index", "Home");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    // await _emailSender.SendEmailAsync(user.Email, "Blog", "Hoşgeldiniz");
                    Guid activationCode = Guid.NewGuid();

                    await _emailSender.SendEmailAsync(user.Email, "Aktivasyon Maili", "<br /><a href = '" + string.Format("https://localhost:44318/Activation/Activation/{0}", activationCode) + "'>Giriş için tıklayınız</a>");
                  
                    return RedirectToAction("Index", "Home");
                                       
                }
                ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi");
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
