using CompanyPortal.Application.Abstractions.User.Dtos;

namespace CompanyPortal.Application.Abstractions.User
{
    public interface ILoginService
    {
        Task<bool> Login(UserDto user);
        Task<bool> CheckRole(string email, string role);
    }
}
