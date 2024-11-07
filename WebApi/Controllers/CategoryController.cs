using Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoryController
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
                var category = await _categoryService.GetById(id);

                return Ok(category);
        }
        
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CategoryBaseRequest request)
        {
            var category = await _categoryService.Add(request);

            return Ok(category);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
                var category = await _categoryService.Delete(id);

                return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryBaseRequest request)
        {
                var category = await _categoryService.Update(id, request);

                return Ok(category);
        }
    }
}
