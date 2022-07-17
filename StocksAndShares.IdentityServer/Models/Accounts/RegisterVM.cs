using System.ComponentModel.DataAnnotations;

namespace StocksAndShares.IdentityServer.Models.Accounts
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string returnUrl { get; set; }
    }
}
