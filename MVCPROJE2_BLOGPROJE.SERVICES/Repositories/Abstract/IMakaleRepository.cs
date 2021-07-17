using MVCPROJE2_BLOGPROJE.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract
{
    public interface IMakaleRepository
    {
        IQueryable<Makale> Makaleler { get; }
        Makale GetById(int id);
        bool MakaleEkle(Makale makale);
        bool MakaleGuncelle(Makale makale);
        bool MakaleSil(int id);
        List<Makale> MakaleUyeDahilEt();
        List<Konu> KonuListesi();
        List<Makale> MakaleKonuDahilEt();
    }
}
