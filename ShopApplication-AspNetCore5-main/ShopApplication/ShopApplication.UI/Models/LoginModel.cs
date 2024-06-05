using System.ComponentModel.DataAnnotations;

namespace ShopApplication.UI.Models
{
    public class LoginModel
    {
        //[Required]
        //public string Username { get; set; } // Username ile giriş, istersek mail vs. de yapabiliriz.

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } // Username ile giriş, istersek mail vs. de yapabiliriz.
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }   
    }
}
