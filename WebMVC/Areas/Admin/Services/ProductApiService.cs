using AutoMapper;
using Datas.Entities;
using Dtos.Products;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WebMVC.Areas.Admin.Services
{
    public interface IProductApiService
    {
        Task<List<ProductDto>> GetAll();
    }

    public class ProductApiService : IProductApiService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductApiService(
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.Query()
                .ToListAsync();

            var rs = _mapper.Map<List<ProductDto>>(products);

            return rs;
        }
    }
}
