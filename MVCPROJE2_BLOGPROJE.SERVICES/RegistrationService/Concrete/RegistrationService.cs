using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.CORE.ViewModels;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Concrete
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<IdentityUser> _userManager;       
        public RegistrationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;            
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {

            IdentityUser user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
