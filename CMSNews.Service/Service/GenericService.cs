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
    public class GenericService<T> : GenericRepository<T> where T : BaseEntity
    {
        public GenericService(dbContext context) : base(context)
        {
        }
    }
}
