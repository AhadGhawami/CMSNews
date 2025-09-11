using CMSNews.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class NewsGroupViewModel
    {
       
        [DisplayName("کد گروه خبری")]
        public Guid NewsGroupId { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("عنوان گروه خبری")]
        public string NewsGroupTitle { get; set; }
        [MaxLength(100)]
        [DisplayName("تصویر")]
        public string? ImageName { get; set; }
        public IEnumerable<tblNews>? Newses { get; set; }
    }
}
