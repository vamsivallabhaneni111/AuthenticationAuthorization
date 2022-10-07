using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace StocksAndShares.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStockAndSharesClientManager _stockAndSharesClient;

        public HomeController(IStockAndSharesClientManager stockAndSharesClient)
        {
            _stockAndSharesClient = stockAndSharesClient;
        }

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
            var secretmessage = await _stockAndSharesClient.GetResourceAsync<string>("/GeoAffect/Home");
            return View("Secret", secretmessage);
        }
    }

}
