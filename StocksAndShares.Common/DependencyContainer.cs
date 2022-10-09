using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StocksAndShares.Api.Gateway.Services.Factory;
using StocksAndShares.Api.Gateway.Services.FundTracker;
using StocksAndShares.Api.Gateway.Services.LiquidFundsApi;

namespace StocksAndShares.Api.Gateway
{
    public class DependencyContainer
    {
        public static void RunServicesConfig(IServiceCollection services)
        {
            // utilities
            services.AddHttpClient<StocksAndSharesHttpClient>();
            services.AddTransient<IStocksAndSharesHttpClient, StocksAndSharesHttpClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // services
            services.AddScoped<ILiquidFundsService, LiquidFundsService>();
            services.AddScoped<IFundTrackerService, FundTrackerService>();
        }   
    }
}
