using CMSNews.Model.Context;
using CMSNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Repository.Repository
{
    public class CommentRepository : GenericRepository<tblComment>, ICommentRepository
    {
        public CommentRepository(dbContext context) : base(context)
        {
        }
    }
}
