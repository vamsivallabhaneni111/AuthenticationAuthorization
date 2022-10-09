
namespace StocksAndShares.Api.Gateway.Services.LiquidFunds
{
    public interface ILiquidFundsService
    {
        public Task<string> GetCurrencyAtLocation();
    }
}
