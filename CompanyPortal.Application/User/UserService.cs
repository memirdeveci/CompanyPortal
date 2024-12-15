using AutoMapper;
using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Application.Abstractions.User.Dtos;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _deptService;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IDepartmentService deptService) 
        {
            _userManager = userManager;
            _mapper = mapper;
            _deptService = deptService;
        }

        public async Task<bool> AddUser(UserDto user)
        {
            if (user is null)
                return false;
            
            try
            {
                user.UserName = user.Name + user.Surname;

                var mappedResult = _mapper.Map<UserDto, AppUser>(user);

                var response = await _userManager.CreateAsync(mappedResult, user.Password);

                var result = await _userManager.AddToRoleAsync(mappedResult, "Employee");

                return response.Succeeded && result.Succeeded;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                
                if (user == null)
                    return false;
                             
                var response = await _userManager.DeleteAsync(user);
                return response.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditUser(ProfileDto user)
        {
            if (user is null)
                return false;

            try
            {
                var userEntity = await _userManager.FindByEmailAsync(user.Email);

                if (userEntity is null)
                    return false;
              
                user.Id = userEntity.Id;
                var mapResult = _mapper.Map(user, userEntity);
                var response = await _userManager.UpdateAsync(mapResult);

                return response.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                if (users is null)
                    return [];

                var mappedResult = _mapper.Map<List<AppUser>, List<UserDto>>(users);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
                return new UserDto();

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user is null)
                    throw new Exception();

                var mappedResult = _mapper.Map<AppUser, UserDto>(user);

                return mappedResult;
            }
            catch (Exception)
            {
                return new UserDto();
            }
        }

        public Task<bool> SoftDeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminDto> GetAdmin(ClaimsPrincipal principal)
        {
            try
            {
                var user = await _userManager.GetUserAsync(principal);

                if (user is null)
                    throw new Exception();

                var mappedResult = _mapper.Map<AppUser, AdminDto>(user);

                return mappedResult;
            }
            catch (Exception)
            {
                return new AdminDto();
            }
        }

        public async Task<ProfileDto> GetProfile(ClaimsPrincipal principal)
        {
            try
            {
                var user = await _userManager.GetUserAsync(principal);

                if (user is null)
                    throw new Exception();

                var mappedResult = _mapper.Map<AppUser, ProfileDto>(user);
                var dept = await _deptService.GetDepartmentById(user.DepartmentId);
                mappedResult.DepartmentName = dept.DeptName;

                return mappedResult;
            }
            catch (Exception)
            {
                return new ProfileDto();
            }
        }

        public async Task<List<AppUser>> GetAllAppUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                if (users is null)
                    return [];

                return users;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<AppUser>> GetAllAppUsersWithIds(List<string> Ids)
        {
            try
            {
                var users = new List<AppUser>();
                foreach (string id in Ids)
                {
                    var user = await _userManager.FindByIdAsync(id);

                    if (user is not null)
                        users.Add(user);
                }
                
                if (users is null)
                    return [];

                return users;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
