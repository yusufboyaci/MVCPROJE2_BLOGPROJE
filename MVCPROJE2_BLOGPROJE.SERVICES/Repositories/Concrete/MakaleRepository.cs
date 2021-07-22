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

        public Makale GetById(int id) => _context.Makaleler.Find(id);

        public async Task<List<Konu>> KonuListesiAsync() => await _context.Konular.ToListAsync();
        
        public async Task<bool> MakaleEkleAsync(Makale makale)
        {
           await _context.Makaleler.AddAsync(makale);
           return await _context.SaveChangesAsync() > 0;
        }

        public bool MakaleGuncelle(Makale makale)
        {
           _context.Makaleler.Update(makale);
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Makale>> MakaleKonuDahilEtAsync() => await _context.Makaleler.Include(x => x.KonularMakaleler).ToListAsync();
      

        public bool MakaleSil(int id)
        {
           _context.Makaleler.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Makale>> MakaleUyeDahilEtAsync() => await _context.Makaleler.Include(x => x.Uye).ToListAsync();
       
    }
}
