using FluentValidation;

namespace Dtos.Accounts
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username ko để trống");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password ko để trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
                .Matches("[a-z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
                .Matches("[0-9]").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt."); ;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName ko để trống");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName ko để trống");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber ko để trống");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email ko để trống")
                .EmailAddress().WithMessage("Email ko đúng định dạng");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Dob phải dưới 100+");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.Confirmpassword)
                {
                    context.AddFailure("Confirm ko đúng");
                }
            });
        }
    }
}
