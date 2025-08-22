using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Repository.Repository
{
    public interface IGenericRepository<T>:IDisposable
    {
        IEnumerable<T> GetAll();
        T GetEntity(Guid id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(Guid id);
        void Save();

    }
}
