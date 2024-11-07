using AutoMapper;
using Datas.Entities;
using Dtos.Accounts;
using Dtos.Categories;
using Dtos.Comments;
using Dtos.Products;

namespace Dtos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AccountDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
