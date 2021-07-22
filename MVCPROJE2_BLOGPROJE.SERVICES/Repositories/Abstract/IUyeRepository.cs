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
        Uye GetById(int id);
       Task<bool> UyeEkleAsync(Uye uye);
        bool UyeGuncelle(Uye uye);
        bool UyeSil(int id);
       Task<bool> UyeKaydetAsync();
    }
}
