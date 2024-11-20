using AutoMapper;
using Datas.Entities;
using Dtos.Categories;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WebMVC.Services
{
    public interface ICategoryClientService
    {
        Task<List<CategoryDto>> GetAll();

    }

    public class CategoryClientService : ICategoryClientService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CategoryClientService(
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.Query()
                .ToListAsync();

            var rs = _mapper.Map<List<CategoryDto>>(categories);

            return rs;
        }


    }
}
