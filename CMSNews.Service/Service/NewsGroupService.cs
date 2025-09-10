using CMSNews.Model.Context;
using CMSNews.Model.Models;
using CMSNews.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Service.Service
{
    public class NewsGroupService : GenericService<tblNewsGroup>, INewsGroupService
    {
        public NewsGroupService(DbCMSNewsContext context) : base(context)
        {
        }
    }
}
