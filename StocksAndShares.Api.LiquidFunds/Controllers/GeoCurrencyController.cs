using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StocksAndShares.Api.LiquidFunds.Controllers
{
    public class GeoCurrencyController : Controller
    {
        
        [Authorize]
        public string CurrencyAtLocation()
        {
            return "1 USD = 70 INR";
        }
    }
}
