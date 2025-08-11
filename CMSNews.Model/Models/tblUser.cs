using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Model.Models
{
    public class tblUser
    {
        public Guid id { get; set; }
        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public IEnumerable<tblNews> Newses { get; set; }

    }
}
