using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
