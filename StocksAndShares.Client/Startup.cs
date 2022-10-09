using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StocksAndShares.Client.ApiGateway.Factory;
using StocksAndShares.Client.ApiGateway.FundTracker;
using StocksAndShares.Client.ApiGateway.LiquidFunds;

namespace StocksAndShares.Client
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services
            services.AddHttpClient<StocksAndSharesHttpClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IStocksAndSharesHttpClient, StocksAndSharesHttpClient>();
            
            services.AddScoped<ILiquidFundsService, LiquidFundsService>();
            services.AddScoped<IFundTrackerService, FundTrackerService>();

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = "oidc";
                config.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:44322/";
                    options.ClientId = "client_id_mvc";
                    options.ClientSecret = "client_secret_mvc";
                    options.SaveTokens = true;
                    options.ResponseType = "code";


                    options.GetClaimsFromUserInfoEndpoint = false;

                    options.Scope.Clear();
                    options.Scope.Add("profile");
                    options.Scope.Add("openid");
                    options.Scope.Add("aum.read");
                    options.Scope.Add("liquid_funds.read");
                    options.Scope.Add("custom.employee_profile");
                });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
