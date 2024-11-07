using Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(LoginRequest request)
        {
            return View();
        }
    }
}
