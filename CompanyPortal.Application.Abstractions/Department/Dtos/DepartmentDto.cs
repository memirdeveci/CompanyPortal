using CompanyPortal.Application.Abstractions.Base;
using CompanyPortal.Domain.Entities;

namespace CompanyPortal.Application.Abstractions.Department.Dtos
{
    public class DepartmentDto : BaseDto
    {
        public string? DeptName { get; set; }
        public string? DeptCode { get; set; }
        public string? DeptDesc { get; set; }
        public string? DeptLogo { get; set; }
        public ICollection<AppUser>? Users { get; set; } = [];
    }
}
