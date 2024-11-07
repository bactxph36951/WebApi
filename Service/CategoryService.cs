using AutoMapper;
using Datas.Entities;
using Dtos.Categories;
using Dtos.Results;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryResult> GetById(int id);
        Task<CategoryResult> Add(CategoryBaseRequest request);
        Task<CategoryResult> Delete(int id);
        Task<CategoryResult> Update(int id, CategoryBaseRequest request);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.Query()
                .Include(x => x.Products)
                .ToListAsync();

            var rs = _mapper.Map<List<CategoryDto>>(categories);

            return rs;
        }

        public async Task<CategoryResult> GetById(int id)
        {
            var category = await _categoryRepository.Query()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return new CategoryResult()
                {
                    Error = "Category ko tồn tại"
                };
            }

            var rs = _mapper.Map<CategoryDto>(category);
            return new CategoryResult()
            {
                Category = rs,
            };
        }

        public async Task<CategoryResult> Add(CategoryBaseRequest request)
        {
            var validatorparam = new CategoryRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new CategoryResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var category = new Category
            {
                Name = request.Name,
                Status = request.Status,
            };

            await _categoryRepository.Add(category);

            var rs = _mapper.Map<CategoryDto>(category);

            return new CategoryResult()
            {
                Category = rs,
            };
        }

        public async Task<CategoryResult> Delete(int id)
        {
            var category = await _categoryRepository.Query()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return new CategoryResult()
                {
                    Error = "Category ko tồn tại"
                };
            }

            await _categoryRepository.Delete(category);

            var rs = _mapper.Map<CategoryDto>(category);

            return new CategoryResult()
            {
                Category = rs,
            };
        }

        public async Task<CategoryResult> Update(int id, CategoryBaseRequest request)
        {
            var validatorparam = new CategoryRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new CategoryResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var category = await _categoryRepository.Query()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return new CategoryResult()
                {
                    Error = "Category ko tồn tại"
                };
            }

            category.Name = request.Name;
            category.Status = request.Status;

            await _categoryRepository.Update(category);

            var rs = _mapper.Map<CategoryDto>(category);

            //var rs = new CategoryDto
            //{
            //    Id = category.Id,
            //    Name = category.Name,
            //    Status = category.Status,
            //    Products = category.Products?.Select(p => new ProductDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Price = p.Price.HasValue ? (double)p.Price.Value : 0.0,
            //        Image = p.Image,
            //        Description = p.Description,
            //        CategoryId = p.CategoryId,
            //        CategoryName = category.Name,
            //        Comments = new List<CommentDto>()
            //    }).ToList()
            //};

            return new CategoryResult()
            {
                Category = rs
            };
        }
    }
}
