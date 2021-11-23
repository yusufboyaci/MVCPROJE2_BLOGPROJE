using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.CORE.ViewModels;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
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
        private readonly IUyeRepository _uyeRepository;
        public RegistrationService(UserManager<IdentityUser> userManager,IUyeRepository uyeRepository)
        {
            _userManager = userManager;
            _uyeRepository = uyeRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {

            IdentityUser user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!_uyeRepository.Roles.Any(x => x.Name == "kullanici"))
            {
                var role = new IdentityRole
                {
                    Name = "kullanici",
                    NormalizedName = "KULLANICI",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                await _uyeRepository.AddRole(role);
            }
            await _userManager.AddToRoleAsync(user, "kullanici");
            return result;
        }
    }
}
