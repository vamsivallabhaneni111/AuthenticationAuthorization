using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StocksAndShares.Api.Aum.Controllers
{
    /// <summary>
    /// Funds affected by Geo-Graphical locations.
    /// </summary>
    [Authorize]
    public class FundTrackerController : Controller
    {
        //[Authorize(Policy = "allow_aum.read_only_user_scope")]
        public IActionResult GetIntelFundAmount()
        {
            try
            {
                return Ok("I am AUM service, I can provide all details of Funds related to Equity");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
