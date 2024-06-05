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
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            //throw new NotImplementedException();

            using (var context = new ShopContext())
            {
                var orders = context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(o => o.Product)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(userId)) // Eğer yukarıdan bir userId gelmiş ise userId ye göre döner.
                {                                  // Eğer userId gelmemiş ise (Yani sisteme admin girişi yapılmış) bütün siparişler döner. 
                    orders = orders.Where(o => o.UserId == userId);
                }

                return orders.ToList();
            }
        }
    }
}
