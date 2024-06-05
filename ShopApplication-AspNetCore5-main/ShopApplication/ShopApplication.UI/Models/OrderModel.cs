using System.ComponentModel.DataAnnotations;

namespace ShopApplication.UI.Models
{
    public class OrderModel // Sipariş kayıt için model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ExpirationMonth { get; set; } // son kullanma ay
        public string ExpirationYear { get; set; } // son kullanma yıl
        public string CardNumber { get; set; } 
        public string CardName { get; set; }
        public string Cvv { get; set; } 
        public CartModel CartModel { get; set; }
          
    }
}
