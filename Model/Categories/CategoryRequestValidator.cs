using FluentValidation;

namespace Dtos.Categories
{
    public class CategoryRequestValidator : AbstractValidator<CategoryBaseRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(20).WithMessage("Tên không được quá 20 kí tự");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái phải là một giá trị hợp lệ trong enum");
        }
    }
}
