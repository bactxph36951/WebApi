using Dtos.Products;

namespace Dtos.Results
{
    public class ProductResult
    {
        public ProductDto? Product { get; set; }
        public string? Error { get; set; }
        public List<string>? Errors { get; set; }
    }
}
