﻿using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Application.Abstractions.User.Dtos;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.User
{
    public interface IUserService
    {
        Task<bool> AddUser(UserDto user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> EditUser(UserDto user);
        Task<UserDto> GetUserById(Guid id);
        Task<List<UserDto>> GetAllUsers();
        Task<bool> SoftDeleteUser(Guid id);
        Task<AdminDto> GetAdmin(ClaimsPrincipal principal);
    }
}
