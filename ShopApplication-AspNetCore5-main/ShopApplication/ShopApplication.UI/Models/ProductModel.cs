using ShopApplication.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApplication.UI.Models
{
    public class ProductModel
    {
        // entities den aldık.
        public int Id { get; set; }
        //[Required] // Nesneye "Name" verilmesini zorunlu kıldık.
        //[StringLength(60, MinimumLength =10, ErrorMessage ="Ürün Adı 10-60 karakter aralığında olmalıdır.")]
        public string Name { get; set; }
        //[Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 20, ErrorMessage = "Ürün açıklaması 20-150 karakter aralığında olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Lütfen ürün fiyatını belirtiniz.")]
        [Range(1,1000)] // Ürün fiyatı 1-1000tl aralığında
        public decimal? Price { get; set; }
        public List<Category> SelectedCategories { get; set; }  // Seçilmiş Kategoriler
    }
}
