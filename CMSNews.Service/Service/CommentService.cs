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
    public class CommentService : GenericService<tblComment>, ICommentService
    {
        public CommentService(IGenericRepository<tblComment> repository) : base(repository)
        {
        }

        // متدهای خاص NewsGroup رو اینجا اضافه کن (اگه داشتی)
    }
}
