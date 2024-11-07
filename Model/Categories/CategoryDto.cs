using Dtos.Products;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dtos.Categories
{
    public class CategoryDto : CategoryBaseRequest
    {
        public int Id { get; set; }
        public string StatusName => Status.GetType()?
            .GetField(Status.ToString())?
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? Status.ToString();
        public List<ProductDto>? Products { get; set; }
    }
}
