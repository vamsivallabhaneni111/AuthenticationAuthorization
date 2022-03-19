using Microsoft.AspNetCore.Mvc;

namespace StocksAndShares.Client.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
