using CompanyPortal.Application.Abstractions.Base;
using CompanyPortal.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyPortal.Application.Abstractions.User.Dtos
{
    public class UserDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? DepartmentId { get; set; }
        public List<SelectListItem>? DepartmentList { get; set; }
    }
}
