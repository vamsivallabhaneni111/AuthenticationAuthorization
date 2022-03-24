using System.Threading.Tasks;

namespace StocksAndShares.Api.Aum.Services
{
    public interface ILiquidFundsService
    {
        Task<string> CurrencyAtLocation();
    }
}
