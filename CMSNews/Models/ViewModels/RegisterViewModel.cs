using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class RegisterViewModel
    {
    
        [Required]
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند")]
        public string ConfirmPassword { get; set; }

    }
}
