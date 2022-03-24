using Microsoft.AspNetCore.Mvc;
using StocksAndShares.Api.Aum.Services;
using System;
using System.Threading.Tasks;

namespace StocksAndShares.Api.Aum.Controllers
{
    /// <summary>
    /// Funds affected by Geo-Graphical locations.
    /// </summary>
    public class GeoAffectController : Controller
    {
        private readonly ILiquidFundsService _liquidFundsService;

        public GeoAffectController(ILiquidFundsService liquidFundsService)
        {
            this._liquidFundsService = liquidFundsService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _liquidFundsService.CurrencyAtLocation());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
