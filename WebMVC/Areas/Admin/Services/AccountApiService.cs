using Datas.Entities;
using Dtos.Accounts;
using Dtos.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebMVC.Areas.Admin.Services
{
    public interface IAccountApiService
    {
        Task<LoginResult> Authenticate(LoginRequest request);
        Task<IdentityResult> Register(RegisterRequest request);
    }

    public class AccountApiService : IAccountApiService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountApiService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResult> Authenticate(LoginRequest request)
        {
            var validatorparam = new LoginRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);
            if (!validateParamResult.IsValid)
            {
                return new LoginResult()
                {
                    Error = "Tài khoản mật khẩu không được để trống"
                };
            }


            var user = await _userManager.FindByNameAsync(request.UserName);
            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);

            if (user == null || !passwordCheck)
            {
                return new LoginResult()
                {
                    Error = "Tài khoản hoặc mật khẩu k đúng"
                };
            }

            return new LoginResult()
            {
                Success = true,
            };

        }

        public async Task<IdentityResult> Register(RegisterRequest request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}
