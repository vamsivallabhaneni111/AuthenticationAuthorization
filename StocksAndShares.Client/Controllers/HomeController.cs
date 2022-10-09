using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StocksAndShares.Client.ApiGateway.FundTracker;
using StocksAndShares.Client.ApiGateway.LiquidFunds;
using System.Threading.Tasks;

namespace StocksAndShares.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILiquidFundsService _liquidFundsService;
        private readonly IFundTrackerService _fundTrackerService; 

        public HomeController(
            ILiquidFundsService liquidFundsService, 
            IFundTrackerService fundTrackerService)
        {
            _liquidFundsService = liquidFundsService;
            _fundTrackerService = fundTrackerService;
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
        public async Task<IActionResult> CurrentNews()
        {
            var currencyAtLocation = await _liquidFundsService.GetCurrencyAtLocation();
            var fundTracker = await _fundTrackerService.GetIntelFundAmount();
            return View("Secret", currencyAtLocation+fundTracker);
        }
    }

}
