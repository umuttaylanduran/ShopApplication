using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        // Genel bütün Dal'larda kullanılacak methodlar burada tanımlanır.
        T GetById(int id);
        T GetOne(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null); // IEnumerable araştır.

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
