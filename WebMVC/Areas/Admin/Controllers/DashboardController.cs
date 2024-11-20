using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = "admin")]

	public class DashboardController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }
    }
}
