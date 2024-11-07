using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Products
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(50).WithMessage("Tên không được quá 50 kí tự");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Giá không được để trống");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Ảnh không được để trống");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả không được để trống");
        }
    }
}
