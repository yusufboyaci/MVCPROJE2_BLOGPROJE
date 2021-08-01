using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract
{
    public interface IRegistrationService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    }
}
