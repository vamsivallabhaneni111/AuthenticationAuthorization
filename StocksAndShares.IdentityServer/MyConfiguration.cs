using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace StocksAndShares.IdentityServer
{
    public static class MyConfiguration
    {
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
                        IdentityServerConstants.StandardScopes.Profile,
                        "employee_profile",
                        "custom.profile",
                    },
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };


        // This is used to craft the Id Token by IdentityServer.
        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),

                new IdentityResources.Profile(),

                new IdentityResource
                {
                    Name = "employee_profile",
                    UserClaims = {"employee_id"},
                },
                new IdentityResource(
                    name: "custom.profile",
                    displayName: "Custom profile",
                    claimTypes: new[] { "status" }),
            };

        // This is used to craft the access Token by IdentityServer.
        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource> {
                new ApiResource("LiquidFunds"),

                new ApiResource("Aum")
            };


        //public static IEnumerable<Scope> Scopes =>
        //    new List<Scope>
        //    {
        //        new Scope
        //        {
        //            Name= "employee_profile",
        //            UserClaims = {"employee_id"},
                    
        //        }
        //    };
    }
}
