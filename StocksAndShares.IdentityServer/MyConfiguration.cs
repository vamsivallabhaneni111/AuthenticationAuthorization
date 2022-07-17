using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace StocksAndShares.IdentityServer
{
    public static class MyConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource> {
                new ApiResource("LiquidFunds"),

                new ApiResource("Aum")
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
                },

                // Registering client for mvc
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("client_secret_mvc".ToSha256())
                    },
                    RedirectUris = { "https://localhost:44362/signin-oidc" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { 
                        "LiquidFunds", 
                        "Aum", 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RequireConsent = false
                }
            };

        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),

                new IdentityResources.Profile()
            };

        //public static IEnumerable<ApiScope> ApiScopes =>
        //    new List<ApiScope>
        //    {
        //        new ApiScope("LiquidFunds", "Engine")
        //    };
    }
}
