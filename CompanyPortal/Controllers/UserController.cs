using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Application.Abstractions.User.Dtos;
using CompanyPortal.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IDepartmentService _deptService;
        private readonly IPhotoService _photoService;

        public UserController(IUserService userService, ILoginService loginService, IDepartmentService deptService, IPhotoService photoService) 
        {
            _userService = userService;
            _loginService = loginService;
            _deptService = deptService;
            _photoService = photoService;
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
        public async Task<IActionResult> CreateUser()
        {
            var depts = await _deptService.GetAllDepartments();
            var items = depts.Select(x => new SelectListItem(){Text = x.DeptName, Value = x.Id.ToString()});
           
            UserDto newUserDto = new()
            {
                DepartmentList = items.ToList(),
            };

            return View(newUserDto);
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
                if (profileDto.ProfilePicture != null)
                {
                    var imageResult = await _photoService.AddPhotoAsync(profileDto.ProfilePicture);
                    profileDto.ProfilePhoto = imageResult.Url.ToString();
                }
                var response = await _userService.EditUser(profileDto);

                if (response)
                    return RedirectToAction("Profile", "User");
            }
            return View();
        }
    }
}
