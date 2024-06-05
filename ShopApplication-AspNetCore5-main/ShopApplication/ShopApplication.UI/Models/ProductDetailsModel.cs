using ShopApplication.Entities;
using System.Collections.Generic;

namespace ShopApplication.UI.Models
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; } // List olarak alıyoruz çünkü bir ürün birden fazla kategoride yer alabilir.  
    }
}
