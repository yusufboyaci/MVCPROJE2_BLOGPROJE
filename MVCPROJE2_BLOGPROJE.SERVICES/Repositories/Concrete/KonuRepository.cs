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
    public class KonuRepository:IKonuRepository
    {
        private readonly ApplicationDbContext _context;
        public KonuRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Konu> Konular => _context.Konular;
    }
}
