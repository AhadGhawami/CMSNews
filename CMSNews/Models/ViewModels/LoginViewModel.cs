using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
