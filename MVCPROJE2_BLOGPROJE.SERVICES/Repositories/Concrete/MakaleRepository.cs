using Microsoft.EntityFrameworkCore;
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
    public class MakaleRepository : IMakaleRepository
    {
        private readonly ApplicationDbContext _context;
        public MakaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Makale> Makaleler => _context.Makaleler;
        
        public async Task<Makale> GetByIdAsync(int id) => await _context.Makaleler.FindAsync(id);

        public async Task<List<Konu>> KonuListesiAsync() => await _context.Konular.ToListAsync();

        public async Task<bool> MakaleEkleAsync(Makale makale)
        {
            await _context.Makaleler.AddAsync(makale);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> MakaleGuncelleAsync(Makale makale)
        {
            await Task.Run(() =>
            {
                _context.Makaleler.Update(makale);
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Makale>> MakaleKonuDahilEtAsync() => await _context.Makaleler.Include(x => x.KonularMakaleler).ToListAsync();


        public async Task<bool> MakaleSilAsync(int id)
        {

            _context.Makaleler.Remove(await GetByIdAsync(id));

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Makale>> MakaleUyeDahilEtAsync() => await _context.Makaleler.Include(x => x.Uye).ToListAsync();

    }
}
