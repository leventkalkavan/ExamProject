using EntityLayer.Entities;
using EntityLayer.Entities.Common;

namespace BusinessLayer.Abstract;

public interface IBaseService<T> : IService<T> where T : BaseEntity
{
    Task<bool> AddAsync(T model);
    bool Remove(T model);
    Task<bool> RemoveAsync(string id);
    bool Update(T model);
    Task<int> SaveAsync();
    IQueryable<T> GetAll(bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);
}