using Microsoft.AspNetCore.Http;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.ViewModels
{
    public class MakaleIndexViewModel
    {

        
        public int KonuID { get; set; }
        [StringLength(300)]
        [Display(Name = "Konular")]
        public string KonuBasliklari { get; set; }
       
        public int MakaleID { get; set; }
        [Display(Name = "Okunma Sayısı")]
        public int? OkunmaSayisi { get; set; }
        [Display(Name = "Makeleniz")]
        public string MakaleIcerigi { get; set; }
        [StringLength(100)]
        [Display(Name = "Başlık")]
        public string MakaleBasligi { get; set; }
        [Display(Name = "Resminiz")]
        public string MakaleResim { get; set; }
        [NotMapped]
        public IFormFile ResimYolu { get; set; }
        public int UyeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Lütfen adınızı yazınız.")]
        public string UyeAd { get; set; }
        [StringLength(50)]
        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Lütfen soyadınızı yazınız.")]
        public string UyeSoyad { get; set; }
    }
}
