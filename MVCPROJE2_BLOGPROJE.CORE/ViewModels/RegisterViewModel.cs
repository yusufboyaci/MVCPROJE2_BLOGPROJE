using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.ViewModels
{
    public class RegisterViewModel
    {      
        [Required(ErrorMessage = "Lütfen Mail alanını boş bırakmayınız")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Lütfen Şifre alanını boş bırakmayınız")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Şifre eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }
}
