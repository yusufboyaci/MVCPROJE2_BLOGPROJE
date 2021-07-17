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


        public List<Konu> KonuListesi() => _context.Konular.ToList();
        
        public bool MakaleEkle(Makale makale)
        {
            _context.Makaleler.Add(makale);
            return _context.SaveChanges() > 0;
        }

        public bool MakaleGuncelle(Makale makale)
        {
            _context.Makaleler.Update(makale);
            return _context.SaveChanges() > 0;
        }

        public List<Makale> MakaleKonuDahilEt() => _context.Makaleler.Include(x => x.KonularMakaleler).ToList();
      

        public bool MakaleSil(int id)
        {
            _context.Makaleler.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public List<Makale> MakaleUyeDahilEt() => _context.Makaleler.Include(x => x.Uye).ToList();
       
    }
}
