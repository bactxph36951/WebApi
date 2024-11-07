using Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productService.GetById(id);

            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] ProductCreateRequest request)
        {
            var product = await _productService.Add(request);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productService.Delete(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, ProductUpdateRequest request)
        {
            var product = await _productService.Update(id, request);

            return Ok(product);
        }
    }
}
