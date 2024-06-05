using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Entities
{
    public class ProductCategory // Product ve Category arasında çoka çok bir ilişki kurmak için oluşturduğum sınıfım. (Junction Table)
    {
        //public int Id { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }    
    }
}
