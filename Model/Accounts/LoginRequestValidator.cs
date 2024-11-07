using FluentValidation;

namespace Dtos.Accounts
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username ko để trống");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password ko để trống");
        }
    }
}
