using Microsoft.EntityFrameworkCore;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Concrete.EntityFramework
{
    public static class SeedDatabase
    {
        // Bu sayfada bir değişiklik yapıldığında Migration güncellemek için : 
        // Powershell Aç --> dotnet ef database drop --> Y --> dotnet ef database update 

        public static void Seed()
        {
            var context = new ShopContext(); // bir context nesnesi ürettik.

            if (context.Database.GetPendingMigrations().Count() == 0) // Eğer bekleyen migration sayısı 0 ise.
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if(context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory); // Listeyi ekleyelim.
                }

                context.SaveChanges();
            }
        }

        private static Category[] Categories =
        {
            new Category() { Name = "Meyve" },
            new Category() { Name = "Sebze"},
            new Category() { Name = "Süt ve Süt Ürünleri"},
        };

        private static Product[] Products =
        {
            new Product() { Name = "Elma", Price= 5, ImageUrl="1.jpg", Description="<p>Doğal kıpkırmızı Amasya elması</p>" },
            new Product() { Name = "Armut", Price= 7, ImageUrl="2.jpg", Description="<p>Organik Armut</p>" },
            new Product() { Name = "Muz", Price= 20, ImageUrl="3.jpg", Description="<p>Hakiki muz, mükemmel!</p>" },
            new Product() { Name = "Çilek", Price= 15, ImageUrl="4.jpg", Description="<p>Taptatlı çilek! Tamamen organik.</p>" },
            new Product() { Name = "Patlıcan", Price= 15, ImageUrl="5.jpg", Description="<p>Patlıcannnn</p>" },
        };

        private static ProductCategory[] ProductCategory =  
        {
            new ProductCategory() { Product = Products[0], Category = Categories[0]},
            new ProductCategory() { Product = Products[1], Category = Categories[0]},
            new ProductCategory() { Product = Products[2], Category = Categories[0]},
            new ProductCategory() { Product = Products[3], Category = Categories[0]},
            new ProductCategory() { Product = Products[4], Category = Categories[1]}
        };

    }
}
