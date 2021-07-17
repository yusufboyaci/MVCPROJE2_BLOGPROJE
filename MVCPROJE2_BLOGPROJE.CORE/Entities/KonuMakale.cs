using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.Entities
{
    public class KonuMakale
    {
        public int KonuID { get; set; }
        public int MakaleID { get; set; }
        public virtual Konu Konu { get; set; }
        public virtual Makale Makale { get; set; }
    }
}
