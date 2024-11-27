using CompanyPortal.Application.Abstractions.Repositories.Department;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Department
{
    public class DepartmentReadRepository : ReadRepository<Domain.Entities.Department>, IDepartmentReadRepository
    {
        public DepartmentReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
