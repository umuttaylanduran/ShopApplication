using ShopApplication.Business.Abstract;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        // Dependency Injection
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Create(Order entity)
        {
            //throw new NotImplementedException();
            _orderDal.Create(entity);
        }

        public List<Order> GetOrders(string userId)
        {
            //throw new NotImplementedException();
            return _orderDal.GetOrders(userId);
        }
    }
}
