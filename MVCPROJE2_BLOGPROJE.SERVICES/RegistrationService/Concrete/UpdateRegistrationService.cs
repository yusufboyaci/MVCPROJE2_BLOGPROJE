using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Concrete
{
    public class UpdateRegistrationService : IUpdateRegistrationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateRegistrationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task UpdatePasswordAsync(string id, string currentPassword, string newPassword)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        public async Task UpdateAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            await _userManager.UpdateAsync(user);
        }
        public async Task UpdateAsync(string id, string mail)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            user.Email = mail;
            user.UserName = mail;
            await _userManager.UpdateAsync(user);
        }
        public async Task RemoveAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

    }
}
