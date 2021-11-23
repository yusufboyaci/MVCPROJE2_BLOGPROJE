using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract
{
    public interface IUyeRepository
    {
        IQueryable<Uye> Uyeler { get; }
        IQueryable<IdentityRole> Roles { get; }
        Task<Uye> GetByIdAsync(int id);
        Task<bool> UyeEkleAsync(Uye uye);
        Task<bool> UyeGuncelleAsync(Uye uye);
        Task<bool> UyeSilAsync(int id);
        Task<bool> UyeKaydetAsync();
        Task<bool> AddRole(IdentityRole role);
    }
}
