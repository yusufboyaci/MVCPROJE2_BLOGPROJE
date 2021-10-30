using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.ViewModels
{
    public class UserWithRolesViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
