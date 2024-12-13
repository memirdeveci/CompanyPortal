using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
