using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace StocksAndShares.IdentityServer
{
    public static class MyConfiguration
    {
        private const string AUM = "aum";
        private const string AUM_Read = "aum.read";
        private const string AUM_Write = "aum.write";
        private const string LiquidFunds = "liquid_funds";
        private const string LiquidFunds_Read = "liquid_funds.read";
        private const string LiquidFunds_Write = "liquid_funds.write";

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                // Registering client
                new Client
                {
                    ClientId = "client_id_aum",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("client_secret_aum".ToSha256())
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
                        AUM_Read,
                        LiquidFunds_Read,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "custom.employee_profile", //custom Id-Scope
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false,
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
                    Name = "custom.employee_profile",
                    UserClaims = {"employee_id"},
                }
            };

        // This is used to craft the access Token by IdentityServer.
        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource> {
                new ApiResource
                {
                    Name = LiquidFunds,
                    DisplayName = "Liquid Funds Api",
                    Scopes = { LiquidFunds_Read, LiquidFunds_Write }
                },

                new ApiResource
                {
                   Name = AUM,
                   DisplayName = "A.U.M Api",
                   Scopes = { AUM_Read, AUM_Write }
                },
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { 
                new ApiScope(AUM_Read),
                new ApiScope(AUM_Write),
                new ApiScope(LiquidFunds_Read),
                new ApiScope(LiquidFunds_Write)
            };
    }
}
