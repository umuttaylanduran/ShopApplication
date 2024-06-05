using Microsoft.EntityFrameworkCore;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ShopApplication.DataAccess.Concrete.EntityFramework
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        // Ortak fonksiyonlar EfCoreGenericRepository.cs dosyasında

        public void DeleteFromCategory(int categoryId, int productId)
        {
            //throw new NotImplementedException();
            using (var context = new ShopContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId); // ExecuteSqlCommand()
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            //throw new NotImplementedException();
            using (var context = new ShopContext())
            {
                return context.Categories
                    .Where(i => i.Id == id)
                    .Include(i=>i.ProductCategories)
                    .ThenInclude(i=>i.Product)
                    .FirstOrDefault();  
            }
        }
    }
}
