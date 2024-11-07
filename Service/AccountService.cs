using Datas.Entities;
using Dtos.Accounts;
using Dtos.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public interface IAccountService
    {
        Task<LoginResult> Authenticate(LoginRequest request);
        Task<RegisterResult> Register(RegisterRequest request);
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public AccountService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<LoginResult> Authenticate(LoginRequest request)
        {
            var validatorparam = new LoginRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);
            if (!validateParamResult.IsValid)
            {
                return new LoginResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new LoginResult()
                {
                    Errors = new List<string>()
                    {
                        "UserName ko tồn tại."
                    },

                };
            }

            var rs = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!rs.Succeeded)
            {
                return new LoginResult()
                {
                    Errors = new List<string>()
                    {
                        "Đăng nhập ko đúng"
                    },
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJSONWebToken(user, roles);

            return new LoginResult()
            {
                Token = token,
            };
        }

        private string GenerateJSONWebToken(AppUser user, IList<string> roles)
        {
            var securityKey = _config["JWT:Key"] ?? "123456789QWERTYUIOPASDFGHJKLZX";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim("role",string.Join(";",roles)),
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
              _config["JWT:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RegisterResult> Register(RegisterRequest request)
        {
            var validatorparam = new RegisterRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new RegisterResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResult()
                {
                    Errors = new List<string>()
                    { 
                        "Email này đã được sử dụng."
                    }
                };
            }

            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
            };

            var registerUser = await _userManager.CreateAsync(user, request.Password);

            return new RegisterResult()
            {
                Success = "Thêm user thành công"
            };
        }
    }
}
