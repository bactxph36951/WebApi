using Dtos.Comments;

namespace Dtos.Products
{
    public class ProductDto : ProductCreateRequest
    {
        public int Id { get; set; }

        public string? CategoryName { get; set; }

        public List<CommentDto>? Comments { get; set; }
    }
}
