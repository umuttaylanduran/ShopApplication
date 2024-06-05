using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        // OrderDal'a ek olarak eklemek istediğimiz method olursa buraya ekleyeceğiz.
        List<Order> GetOrders(string userId);
    }
}
