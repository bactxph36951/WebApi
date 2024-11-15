using Dtos.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
}
