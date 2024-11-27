using CompanyPortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table {  get; }
    }
}
