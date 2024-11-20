using Datas.Entities;
using Dtos.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Areas.Admin.Services;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("admin/[controller]")]
	[Authorize(Roles = "admin")]
    public class AccountController : Controller
	{
        private readonly IAccountApiService _accountApiService;
		private readonly UserManager<AppUser> _userManager;

		public AccountController(IAccountApiService accountApiService,
            UserManager<AppUser> userManager)
        {
            _accountApiService = accountApiService;
			_userManager = userManager;
		}

        [HttpGet("Login")]
		[AllowAnonymous]
        public async Task<IActionResult> Login()
        {
			ClaimsPrincipal claimUser = HttpContext.User;

			if (claimUser.Identity.IsAuthenticated)
			{
				return View("Dashboard/Index");
			}

			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			//Response.Cookies.Delete(".AspNetCore.Identity.Application");

			return View();
        }

        [HttpPost("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _accountApiService.Authenticate(request);

			if (!result.Success)
            {
				ModelState.AddModelError(string.Empty, result.Error);

				return View(request);
			}

			var user = await _userManager.FindByNameAsync(request.UserName);

			if (!await _userManager.IsInRoleAsync(user, "admin"))
			{
				ModelState.AddModelError(string.Empty, "Tài khoản không có quyền truy cập");

				return View(request);
			}
			else
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


				//return View("~/Areas/Admin/Views/Dashboard/Index");
				return RedirectToAction("Index", "Dashboard");
			}
		}

        // HttpPost - Đăng xuất
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return RedirectToAction("Login", "Account");
        }
    }
}
