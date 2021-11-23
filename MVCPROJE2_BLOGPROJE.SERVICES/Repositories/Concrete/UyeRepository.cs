using Microsoft.AspNetCore.Identity;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.DATAACCESS.Context;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Concrete
{
    public class UyeRepository : IUyeRepository
    {
        private readonly ApplicationDbContext _context;
        public UyeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Uye> Uyeler => _context.Uyeler;
        public IQueryable<IdentityRole> Roles => _context.Roles;
        public async Task<Uye> GetByIdAsync(int id)
        {
            return await _context.Uyeler.FindAsync(id);
        }
        public async Task<bool> UyeEkleAsync(Uye uye)
        {
            await _context.Uyeler.AddAsync(uye);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UyeGuncelleAsync(Uye uye)
        {
            await Task.Run(() =>
            {
                _context.Uyeler.Update(uye);
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UyeKaydetAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> UyeSilAsync(int id)
        {
            _context.Uyeler.Remove(await GetByIdAsync(id));
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddRole(IdentityRole role)
        {
            await _context.Roles.AddAsync(role);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
