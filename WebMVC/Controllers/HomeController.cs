using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeClientService _homeClientService;

        public HomeController(ILogger<HomeController> logger,
            IHomeClientService homeClientService)
        {
            _logger = logger;
            _homeClientService = homeClientService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var rs = await _homeClientService.GetPaging(page);

            return View(rs);
        }
        
        public async Task<IActionResult> SanPhamTheoLoai(string categoryName)
        {
            var rs = await _homeClientService.GetByCategory(categoryName);

            return View(rs);
        }
    }
}
