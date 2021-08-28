using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCPROJE2_BLOGPROJE.CORE.Entities
{
   public class Makale
    {
        public int ID { get; set; }
        [Display(Name ="Okunma Sayısı")]
        public int? OkunmaSayisi { get; set; }
        [Display(Name ="Makeleniz")]
        public string MakaleIcerigi { get; set; }
        [StringLength(100)]
        [Display(Name ="Başlık")]
        public string MakaleBasligi { get; set; }
        [Display(Name ="Resim")]
        public string MakaleResim { get; set; }
        [NotMapped]
        public IFormFile ResimYolu { get; set; }
        public int UyeID { get; set; }//FK
        public int KonuID { get; set; }
        public virtual Uye Uye { get; set; }
        public virtual IEnumerable<KonuMakale> KonularMakaleler { get; set; }
    }
}
