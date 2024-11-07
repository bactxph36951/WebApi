using AutoMapper;
using Datas.Entities;
using Dtos.Comments;
using Dtos.Products;
using Dtos.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductResult> GetById(int id);
        Task<ProductResult> Add(ProductCreateRequest request);
        Task<ProductResult> Delete(int id);
        Task<ProductResult> Update(int id, ProductUpdateRequest request);
    }
    public class ProductService : IProductService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.Query()
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .ToListAsync();

            var rs = _mapper.Map<List<ProductDto>>(products);

            return rs;
        }

        public async Task<ProductResult> GetById(int id)
        {
            var product = await _productRepository.Query()
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return new ProductResult()
                {
                    Error = "Product ko tồn tại",
                };
            }

            var rs = _mapper.Map<ProductDto>(product);

            return new ProductResult
            {
                Product = rs,
            };
        }

        public async Task<ProductResult> Add(ProductCreateRequest request)
        {
            var validatorparam = new ProductCreateRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new ProductResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Image = request.Image,
                Description = request.Description,
                CategoryId = request.CategoryId,
            };

            await _productRepository.Add(product);

            var rs = _mapper.Map<ProductDto>(product);

            return new ProductResult()
            {
                Product = rs
            };
        }

        public async Task<ProductResult> Delete(int id)
        {
            var product = await _productRepository.Query()
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return new ProductResult()
                {
                    Error = "Product ko tồn tại"
                };
            }

            await _productRepository.Delete(product);

            var rs = _mapper.Map<ProductDto>(product);

            return new ProductResult()
            {
                Product = rs
            };
        }

        public async Task<ProductResult> Update(int id, ProductUpdateRequest request)
        {
            var validatorparam = new ProductUpdateRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new ProductResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var product = await _productRepository.Query()
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return new ProductResult()
                {
                    Error = "Product ko tồn tại"
                };
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Image = request.Image;
            product.Description = request.Description;

            await _productRepository.Update(product);

            var rs = _mapper.Map<ProductDto>(product);

            return new ProductResult()
            {
                Product = rs,
            };
        }
    }
}
