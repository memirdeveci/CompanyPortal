﻿using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Application.Abstractions.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public UserController(IUserService userService, ILoginService loginService) 
        {
            _userService = userService;
            _loginService = loginService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            if(!ModelState.IsValid) return View(userDto);

            var result = await _loginService.Login(userDto);
            var isAdmin = await _loginService.CheckRole(userDto.Email, "Admin");
            if (result)
            {
                if (isAdmin)
                    return RedirectToAction("AdminPanel", "User");
                else
                    return RedirectToAction("Profile", "User");
            }
           
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if(ModelState.IsValid)
            {
                var response = await _userService.AddUser(userDto);
                if (response)
                {
                    return RedirectToAction("ListUser", "User");
                }
            }
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUser()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminPanel()
        {
            var admin = await _userService.GetAdmin(User);
            return View(admin);
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetProfile(User);
            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userService.GetProfile(User);
            return View(user); 
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EditProfile(ProfileDto profileDto)
        {
            if(ModelState.IsValid)
            {
                var response = await _userService.EditUser(profileDto);
                if (response)
                    return RedirectToAction("Profile", "User");
            }
            return View();
        }
    }
}
