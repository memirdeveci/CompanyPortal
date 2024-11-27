using CompanyPortal.Application.Abstractions.Repositories.Department;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Department
{
    public class DepartmentWriteRepository : WriteRepository<Domain.Entities.Department>, IDepartmentWriteRepository
    {
        public DepartmentWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
