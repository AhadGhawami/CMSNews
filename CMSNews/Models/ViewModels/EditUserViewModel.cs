using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class EditUserViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ثبت‌نام")]
        public DateTime RegisterDate { get; set; }
    }
}
