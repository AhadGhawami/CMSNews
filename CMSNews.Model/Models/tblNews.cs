using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Model.Models
{
    public class tblNews : BaseEntity
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
