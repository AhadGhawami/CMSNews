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
    public class UserService : GenericService<tblUser>, IUserService
    {
        public UserService(IGenericRepository<tblUser> repository) : base(repository)
        {
        }

        // متدهای خاص NewsGroup رو اینجا اضافه کن (اگه داشتی)
    }
}
