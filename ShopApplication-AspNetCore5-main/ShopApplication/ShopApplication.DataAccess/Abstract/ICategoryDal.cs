using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>
    {
        // CategoryDal'a ek olarak eklemek istediğimiz method olursa buraya ekleyeceğiz.

        Category GetByIdWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
