using CMSNews.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Model.Context
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }

        public DbSet<tblNews> tblNews { get; set; }
        public DbSet<tblNewsGroup> tblNewsGroup { get; set; }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblComment> tblComment { get; set; }
    }
}
