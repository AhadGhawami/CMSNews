using CMSNews.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class NewsViewModel
    {
        [DisplayName("کد خبر")]
        public Guid NewsId { get; set; }
        [Required]
        [MaxLength(300)]
        [DisplayName("عنوان خبر")]
        public string NewsTitle { get; set; }
        [Required]
        [DisplayName("توضیحات خبر")]
        public string Description { get; set; }
        [MaxLength(100)]
        [DisplayName("تصویر خبر")]
        public string ImageName { get; set; }
        [DisplayName("تاریخ درج")]
        public DateTime RegisterDate { get; set; }
        [DisplayName("فعال / غیرفعال")]
        public bool IsActive { get; set; }
        [DisplayName("تعداد بازدید")]
        public int? See { get; set; }
        [DisplayName("تعداد لایک")]
        public int? Like { get; set; }
        [DisplayName("گروه خبری")]
        public Guid NewsGroupId { get; set; }
        [DisplayName("کاربر ثبت‌کننده")]
        public Guid UserId { get; set; }
        public tblUser User { get; set; }
        public tblNewsGroup NewsGroup { get; set; }
        public IEnumerable<tblComment>? Comments { get; set; }
    }
}
