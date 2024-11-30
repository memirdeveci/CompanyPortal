using CompanyPortal.Application.Abstractions.Base;
using CompanyPortal.Common.Enums;

namespace CompanyPortal.Application.Abstractions.User.Dtos
{
    public class ProfileDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Position { get; set; }
        public string? City { get; set; }
        public string? Education { get; set; }
        public string? DepartmentName { get; set; }
    }
}
