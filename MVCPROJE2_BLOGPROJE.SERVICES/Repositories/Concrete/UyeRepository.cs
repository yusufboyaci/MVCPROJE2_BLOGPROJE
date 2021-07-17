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

        public Uye GetById(int id)
        {
            return _context.Uyeler.Find(id);
        }

        public bool UyeEkle(Uye uye)
        {
            _context.Uyeler.Add(uye);
           // _context.Makaleler.Where(x => x.UyeID == x.ID);
            return _context.SaveChanges() > 0;
        }

        public bool UyeGuncelle(Uye uye)
        {
            _context.Uyeler.Update(uye);
            return _context.SaveChanges() > 0;
        }

        public bool UyeKaydet() => _context.SaveChanges() > 0;

        public bool UyeSil(int id)
        {
            _context.Uyeler.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }
    }
}
