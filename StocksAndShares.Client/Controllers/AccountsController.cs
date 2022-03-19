using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StocksAndShares.Client.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StocksAndShares.Client.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private readonly Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "admin", "admin123" }
        };
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            bool isValidCred = users.Any(x => x.Key.Equals(login.UserName, StringComparison.OrdinalIgnoreCase)
                && x.Value.Equals(login.Password, StringComparison.OrdinalIgnoreCase));
            
            if (!isValidCred)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, login.UserName),
                new Claim(ClaimTypes.Name, login.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties() { IsPersistent = login.RememberMe });

            return LocalRedirect(login.returnUrl ?? "/Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
