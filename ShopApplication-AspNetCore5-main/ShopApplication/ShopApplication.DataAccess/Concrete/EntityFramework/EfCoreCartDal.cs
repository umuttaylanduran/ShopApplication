using Microsoft.EntityFrameworkCore;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Concrete.EntityFramework
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {
        public override void Update (Cart entity) // override method
        {
            //base.Update (Entity);

            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public Cart GetByUserId(string userId)
        {
            //throw new NotImplementedException();

            using (var context = new ShopContext())
            {
                return context
                            .Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            //throw new NotImplementedException();
            // Sql Sorgularını yazalım
            using (var context = new ShopContext())
            {
                var query = @"Delete from CartItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlRaw(query, cartId, productId); // p0 = cartId, p1 = productId
            }
        }

        public void ClearCart(int cartId)
        {
            //throw new NotImplementedException();
            // Sql Sorgularını yazalım
            using (var context = new ShopContext())
            {
                var query = @"Delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlRaw(query, cartId); // p0 = cartId
            }
        }
    }
}
