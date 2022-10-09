using System.Threading.Tasks;

namespace StocksAndShares.Client.ApiGateway.FundTracker
{
    public interface IFundTrackerService
    {
        Task<string> GetIntelFundAmount();
    }
}
