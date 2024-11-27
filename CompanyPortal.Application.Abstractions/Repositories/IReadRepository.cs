using CompanyPortal.Domain.Entities.Common;
using System.Linq.Expressions;

namespace CompanyPortal.Application.Abstractions.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetQueryable(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
    }
}
