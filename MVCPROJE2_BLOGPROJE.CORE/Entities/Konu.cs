using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.Entities
{
    public class Konu
    {
        [Key]
        public int ID { get; set; }
        [StringLength(300)]
        [Display(Name ="Konular")]
        public string KonuBasliklari { get; set; }
        public virtual IEnumerable<UyeKonu> UyelerKonular { get; set; }
        public virtual IEnumerable<KonuMakale> KonularMakaleler { get; set; }
    }
}
