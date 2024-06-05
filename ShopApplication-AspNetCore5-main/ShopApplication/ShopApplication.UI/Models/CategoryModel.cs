using ShopApplication.Entities;
using System.Collections.Generic;

namespace ShopApplication.UI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; } // Kategori içindeki Productları temsil eden Listemiz.
    }
}
