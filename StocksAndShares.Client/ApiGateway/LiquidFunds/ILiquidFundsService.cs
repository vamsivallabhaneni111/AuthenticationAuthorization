using System.Threading.Tasks;

namespace StocksAndShares.Client.ApiGateway.LiquidFunds
{
    public interface ILiquidFundsService
    {
        public Task<string> GetCurrencyAtLocation();
    }
}
