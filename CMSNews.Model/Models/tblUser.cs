using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Model.Models
{
    public class tblUser : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت‌نام")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "اخبار ثبت‌شده")]
        public ICollection<tblNews>? Newses { get; set; }

    }
}
