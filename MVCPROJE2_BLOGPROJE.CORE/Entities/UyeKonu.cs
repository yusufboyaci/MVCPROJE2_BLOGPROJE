using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.Entities
{
    public class UyeKonu
    {
        public int UyeID { get; set; }
        public int KonuID { get; set; }
        public virtual Uye Uye { get; set; }
        public virtual Konu Konu { get; set; }
    }
}
