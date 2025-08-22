using CMSNews.Model.Context;
using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Repository.Repository
{
    public class NewsGroupRepository : GenericRepository<tblNewsGroup>, INewsGroupRepository
    {
        public NewsGroupRepository(dbContext context) : base(context)
        {
        }
    }
}
