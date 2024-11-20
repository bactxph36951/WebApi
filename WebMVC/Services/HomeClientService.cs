using AutoMapper;
using Azure;
using Datas.Entities;
using Dtos.Products;
using Microsoft.EntityFrameworkCore;
using Repositories;
using X.PagedList;

namespace WebMVC.Services
{
    public interface IHomeClientService
    {
        Task<PagedList<ProductDto>> GetPaging(int? page);
        Task<List<ProductDto>> GetByCategory(string categoryName);

    }

    public class HomeClientService : IHomeClientService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public HomeClientService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<ProductDto>> GetPaging(int? page)
        {

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = await _productRepository.Query()
                .AsNoTracking()
                .ToListAsync();

            var rs = _mapper.Map<List<ProductDto>>(products);

            PagedList<ProductDto> lst = new PagedList<ProductDto>(rs, pageNumber, pageSize);

            return lst;
        }
        
        public async Task<List<ProductDto>> GetByCategory(string categoryName)
        {
            var products = await _productRepository.Query()
                .Where(x => x.Category.Name == categoryName)
                .ToListAsync();

            var rs = _mapper.Map<List<ProductDto>>(products);

            return rs;
        }
    }
}
