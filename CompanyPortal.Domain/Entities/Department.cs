using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string? DeptName { get; set; }
        public string? DeptCode { get; set; }
        public string? DeptDesc { get; set; }
        public string? DeptLogo { get; set; }

        //Connections
        public ICollection<AppUser>? Users { get; set; }
    }
}
