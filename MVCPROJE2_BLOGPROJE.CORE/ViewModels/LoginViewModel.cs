using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.CORE.ViewModels
{
    public class LoginViewModel
    {       
        [Required(ErrorMessage ="Lütfen mail adresinizi giriniz.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Lütfen şifre alanını boş bırakmayınız.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
