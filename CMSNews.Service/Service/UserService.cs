using CMSNews.Model.Context;
using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Service.Service
{
    public class UserService : GenericService<tblUser>, IUserService
    {
        public UserService(DbCMSNewsContext context) : base(context)
        {
        }
    }
}
