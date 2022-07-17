using Microsoft.AspNetCore.Identity;

namespace StocksAndShares.IdentityServer.Data
{
    public class SeedingUsers
    {
        public static IdentityUser GetUser => new IdentityUser("Bob");
    }
}
