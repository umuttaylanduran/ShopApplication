using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Entities
{
    public class Cart
    {
        public int Id { get; set; } 
        public string UserId { get; set; }

        public List<CartItem> CartItems { get; set; } // Cart içinde olan bilgileri Liste çeklinde çağırmak için
    }
}
