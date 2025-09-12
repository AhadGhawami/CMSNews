using CMSNews.Model.Models;
using CMSNews.Repository.Repository;
using CMSNews.Service.Service;

public class GenericService<T> : IGenericService<T> where T : BaseEntity
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public IEnumerable<T> GetAll() => _repository.GetAll();
    public T GetEntity(Guid id) => _repository.GetEntity(id);
    public bool Add(T entity) => _repository.Add(entity);
    public bool Update(T entity) => _repository.Update(entity);
    public bool Delete(T entity) => _repository.Delete(entity);
    public bool Delete(Guid id) => _repository.Delete(id);
    public void Save() => _repository.Save();
}