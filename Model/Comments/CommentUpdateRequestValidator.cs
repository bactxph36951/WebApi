using FluentValidation;

namespace Dtos.Comments
{
    public class CommentUpdateRequestValidator : AbstractValidator<CommentUpdateRequest>
    {
        public CommentUpdateRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Bình luận k được để trống");
        }
    }
}
