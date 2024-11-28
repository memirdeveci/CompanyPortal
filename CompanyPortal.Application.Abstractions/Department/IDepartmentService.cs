using CompanyPortal.Application.Abstractions.Department.Dtos;

namespace CompanyPortal.Application.Abstractions.Department
{
    public interface IDepartmentService
    {
        Task<bool> AddDepartment(DepartmentDto department);
        Task<bool> DeleteDepartment(Guid id);
        Task<bool> EditDepartment(DepartmentDto department);
        Task<DepartmentDto> GetDepartmentById(Guid id);
        Task<List<DepartmentDto>> GetAllDepartments();
        Task<bool> SoftDeleteDepartment(Guid id);
    }
}
