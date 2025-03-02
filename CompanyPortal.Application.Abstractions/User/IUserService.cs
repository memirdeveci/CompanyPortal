﻿using CompanyPortal.Application.Abstractions.User.Dtos;
using CompanyPortal.Domain.Entities;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.User
{
    public interface IUserService
    {
        Task<bool> AddUser(UserDto user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> EditUser(ProfileDto user);
        Task<UserDto> GetUserById(Guid id);
        Task<List<UserDto>> GetAllUsers();
        Task<bool> SoftDeleteUser(Guid id);
        Task<AdminDto> GetAdmin(ClaimsPrincipal principal);
        Task<ProfileDto> GetProfile(ClaimsPrincipal principal);
        Task<List<AppUser>> GetAllAppUsers();
        Task<List<AppUser>> GetAllAppUsersWithIds(List<string> Ids);
        Task<List<AppUser>> GetAllAppUsersWoutCurrent(ClaimsPrincipal principal);
    }
}
