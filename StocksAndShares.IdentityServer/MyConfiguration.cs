using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace StocksAndShares.IdentityServer
{
    public static class MyConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource> {
                new ApiResource("LiquidFunds")
            };

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                // Registering client
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("client_secret".ToSha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "LiquidFunds" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("LiquidFunds", "Engine")
            };
    }
}
