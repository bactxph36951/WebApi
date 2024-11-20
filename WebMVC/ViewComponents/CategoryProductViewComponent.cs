using Datas.Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using WebMVC.Services;

namespace WebMVC.ViewComponents
{
    public class CategoryProductViewComponent : ViewComponent
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryProductViewComponent(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var rs = _categoryRepository.Query().Where(x=>x.Status == 0).ToList();

            return View(rs);
        }
    }
}
