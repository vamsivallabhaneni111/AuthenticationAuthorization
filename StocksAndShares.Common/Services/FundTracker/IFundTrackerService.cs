using System;
using System.Collections.Generic;
using System.Text;

namespace StocksAndShares.Api.Gateway.Services.FundTracker
{
    public interface IFundTrackerService
    {
        Task<string> GetIntelFundAmount();
    }
}
