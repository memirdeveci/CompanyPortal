using AutoMapper;
using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Application.Abstractions.User.Dtos;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.Application.User
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public LoginService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<bool> CheckRole(string email, string role)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(role))
                return false;

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    throw new Exception();

                var response = await _userManager.IsInRoleAsync(user, role);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Login(UserDto userDto)
        {
            if (userDto is null)
                return false;

            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);

                if (user == null)
                    throw new Exception();

                var response = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

                return response.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
