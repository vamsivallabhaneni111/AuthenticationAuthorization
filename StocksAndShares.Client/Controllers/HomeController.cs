using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace StocksAndShares.Client.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Landing()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Secret()
        {

            var idToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var tokenHandler = new JwtSecurityTokenHandler();
            var idTokenDesrialsed = tokenHandler.ReadJwtToken(idToken);
            var accessTokenDesrialsed = tokenHandler.ReadJwtToken(accessToken);
            //var refreshTokenDesrialsed = tokenHandler.ReadJwtToken(refreshToken);
            return View();
        }
    }
}
