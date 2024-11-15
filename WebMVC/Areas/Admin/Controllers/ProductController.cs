using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
	[Route("admin/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductApiService _productApiService;

        public ProductController(
            IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }
        public async Task<IActionResult> Index()
        {
            var rs = await _productApiService.GetAll();

            return View(rs);
        }
    }
}
