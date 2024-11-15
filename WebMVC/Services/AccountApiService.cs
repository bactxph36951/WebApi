using Datas.Entities;
using Dtos.Accounts;
using Microsoft.AspNetCore.Identity;

namespace WebMVC.Services
{
    public interface IAccountApiService
    {
        Task<SignInResult> Authenticate(LoginRequest request);
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

        public async Task<SignInResult> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: false);

            return result;
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
