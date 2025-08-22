using CMSNews.Model.Context;
using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Service.Service
{
    public class NewsService : GenericService<tblNews>, INewsService
    {
        public NewsService(dbContext context) : base(context)
        {
        }
    }
}
