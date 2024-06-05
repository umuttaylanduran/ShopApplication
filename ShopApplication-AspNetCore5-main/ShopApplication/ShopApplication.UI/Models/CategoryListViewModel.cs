using ShopApplication.Entities;
using System.Collections.Generic;

namespace ShopApplication.UI.Models
{
    public class CategoryListViewModel
    {
        public string SelectedCategory { get; set; }    // Seçilmiş Kategori
        public List<Category> Categories { get; set; }  
    }
}
