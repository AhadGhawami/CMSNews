using CMSNews.Model.Context;
using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Service.Service
{
    public class CommentService : GenericService<tblComment>, ICommentService
    {
        public CommentService(DbCMSNewsContext context) : base(context)
        {
        }
    }
}
