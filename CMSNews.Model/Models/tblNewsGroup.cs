using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Model.Models
{
    public class tblNewsGroup
    {
        public Guid id { get; set; }
        [Required]
        [MaxLength(200)]
        public string NewsGroupTitle { get; set; }
        [MaxLength(100)]
        public string ImageName { get; set; }
        public IEnumerable<tblNews> Newses { get; set; }
    }
}
