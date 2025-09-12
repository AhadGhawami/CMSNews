using CMSNews.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class NewsViewModel
    {
        [Key]
        public Guid NewsId { get; set; }
        [Required]
        [MaxLength(300)]
        public string NewsTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImageName { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int See { get; set; }
        [Required]
        public int Like { get; set; }
        [Required]
        public Guid tblNewsGroupId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public tblUser User { get; set; }
        public tblNewsGroup NewsGroup { get; set; }
        public IEnumerable<tblComment> Comments { get; set; }
    }
}
