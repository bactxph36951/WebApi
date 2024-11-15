using AutoMapper;
using Datas.Entities;
using Datas.Enums;
using Dtos.Categories;
using Dtos.Results;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebMVC.Services
{
    public interface ICategoryApiService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryResult> Add(CategoryBaseRequest request);
        Task<List<SelectListItem>> GetAllStatus();
        Task<CategoryResult> Delete(int id);
        Task<CategoryResult> GetById(int id);
        Task<CategoryResult> Update(int id, CategoryBaseRequest request);
    }

    public class CategoryApiService : ICategoryApiService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryApiService(
            IRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<SelectListItem>> GetAllStatus()
        {
            var statusList = Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.GetType().GetField(e.ToString())?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
                })
                .ToList();

            return statusList;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.Query()
                .ToListAsync();

            var rs = _mapper.Map<List<CategoryDto>>(categories);

            return rs;
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

            return new CategoryResult()
            {
                Category = rs
            };
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

            var statusList = Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString(),
                    Selected = s == category.Status // Đánh dấu trạng thái đã chọn
                })
                .ToList();

            rs.StatusList = statusList;

            return new CategoryResult()
            {
                Category = rs
            };
        }
    }
}
