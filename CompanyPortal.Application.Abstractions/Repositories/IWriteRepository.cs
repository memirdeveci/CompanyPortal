using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Application.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        bool Add(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Remove(T entity);
        Task<bool> RemoveAsync(Guid id);
        bool RemoveRange(List<T> entities);
        bool Update(T entity);
        bool UpdateRange(List<T> entities);
        Task<int> SaveAsync();
    }
}
