using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity); // sipariş oluşturur
        List<Order> GetOrders(string userId); // bütün siparişleri liste halinde döndürür.
    }
}
