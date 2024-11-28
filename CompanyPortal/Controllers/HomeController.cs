using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentService _deptService;

        public HomeController(ILogger<HomeController> logger, IDepartmentService deptService)
        {
            _logger = logger;
            _deptService = deptService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListDept()
        {
            var depts = await _deptService.GetAllDepartments();
            return View(depts);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateDept()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDept(DepartmentDto deptDto)
        {
            if(ModelState.IsValid)
            {
                var x = await _deptService.AddDepartment(deptDto);
                return RedirectToAction("ListDept", "Home");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
