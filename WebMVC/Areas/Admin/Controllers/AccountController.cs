using Dtos.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Services;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("admin/[controller]")]
    public class AccountController : Controller
	{
        private readonly IAccountApiService _accountApiService;

        public AccountController(IAccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _accountApiService.Authenticate(request);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.UserName),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);


                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }


            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
		}

        // HttpPost - Đăng xuất
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
    }
}
