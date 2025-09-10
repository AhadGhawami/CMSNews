using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNews.Model.Models;
using CMSNews.Model.Context;

namespace CMSNews.Repository.Repository
{
    public class UserRepository : GenericRepository<tblUser>, IUserRepository
    {
        public UserRepository(DbCMSNewsContext context) : base(context)
        {
        }
    }
}
