using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Home()
        {
            try
            {
                return Ok("I am AUM service, I can provide all details of Geo-Effects related to Equity");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get this from LiquidFundsService API
        public async Task<IActionResult> ShowCurrencyAtLocation()
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
