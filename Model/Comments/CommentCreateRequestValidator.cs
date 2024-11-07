using FluentValidation;

namespace Dtos.Comments
{
    public class CommentCreateRequestValidator : AbstractValidator<CommentCreateRequest>
    {
        public CommentCreateRequestValidator()
        {
            RuleFor(x=>x.Description)
                .NotEmpty().WithMessage("Bình luận k được để trống");

            RuleFor(x => x.ProductId)
                .GreaterThanOrEqualTo(0).WithMessage("Sản phẩm phải là số không âm");
        }
    }
}
