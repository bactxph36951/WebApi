using Dtos.Categories;

namespace Dtos.Results
{
    public class CategoryResult
    {
        public CategoryDto? Category { get; set; }
        public string? Error { get; set; }
        public List<string>? Errors { get; set; }
    }
}
