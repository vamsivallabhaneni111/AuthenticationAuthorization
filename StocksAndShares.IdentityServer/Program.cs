using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StocksAndShares.IdentityServer.Data;

namespace StocksAndShares.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DI is ready when we call create host builder.
            var host = CreateHostBuilder(args).Build();

            // use DI to inject/seeding of new users.
            using (var scope = host.Services.CreateScope())
            {
               var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                userManager.CreateAsync(SeedingUsers.GetUser, "password").GetAwaiter().GetResult();
            }
                
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
