using Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Services;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;

        public CategoryController(
            ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            var rs = new CategoryDto
            {
                StatusList = await _categoryApiService.GetAllStatus()
            };
            return View(rs);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CategoryBaseRequest request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var category = await _categoryApiService.Add(request);


            return RedirectToAction("Index");
        }

        [HttpPost()]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryApiService.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryApiService.GetById(id);

            var rs = category.Category;
            return View(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryBaseRequest request)
        {
            var category = await _categoryApiService.Update(id, request);
            return RedirectToAction("Index");
        }
    }
}
