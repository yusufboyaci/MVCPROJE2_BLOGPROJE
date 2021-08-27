using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.Entities
{
   public class Uye
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="Lütfen adınızı yazınız.")]
        public string Ad { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="Lütfen soyadınızı yazınız.")]
        public string Soyad { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage ="Lütfen kullanıcı adını yazınız.")]
        public string KullaniciAdi { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(500)]
        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        public string MailAdresi { get; set; }
        public string KullaniciAciklama { get; set; }
        public bool IsActive { get; set; }

        public string KullaniciResim { get; set; }
        [NotMapped]
        public IFormFile KullaniciResimYolu { get; set; }
        public virtual IEnumerable<Makale> Makaleler { get; set; }
        public virtual IEnumerable<UyeKonu> UyelerKonular { get; set; }

    }
}
