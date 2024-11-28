using CompanyPortal.Application.Abstractions.Base;

namespace CompanyPortal.Application.Abstractions.User.Dtos
{
    public class AdminDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePhoto { get; set; }
    }
}
