using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract
{
    public interface IUpdateRegistrationService
    {
        Task UpdatePasswordAsync(string id, string currentPassword, string newPassword);
        Task UpdateAsync(string id);
        Task RemoveAsync(string id);
    }
}
