using CompanyPortal.Application.Abstractions.User.Dtos;
using CompanyPortal.Domain.Entities;
using CompanyPortal.Persistance.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public UserController(UserManager<AppUser> userManager, AppDbContext context) 
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            //User daha önce eklenmiş mi diye kontrol etmek lazım

            if(ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    Id = Guid.NewGuid(),
                    Name = userDto.Name,
                    Surname = userDto.Surname,
                    Email = userDto.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, userDto.Password);
                if (result.Succeeded)
                {
                    _context.Update(appUser);
                    _context.SaveChanges();
                    return RedirectToAction("ListUser", "User");
                }
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult ListUser()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}
