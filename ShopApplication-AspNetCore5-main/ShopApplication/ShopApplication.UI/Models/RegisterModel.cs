using System.ComponentModel.DataAnnotations;

namespace ShopApplication.UI.Models
{
    public class RegisterModel
    {
        [Required] // zorunluluk
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Şifreniz en az 6 karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor.")] // RePassword doğrulama
        [DataType(DataType.Password)]
        [Required]
        public string RePassword { get; set; }  // parola doğrulama

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }   

    }
}
