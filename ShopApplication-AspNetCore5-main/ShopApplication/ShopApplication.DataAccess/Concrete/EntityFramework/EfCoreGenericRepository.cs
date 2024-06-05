using Microsoft.EntityFrameworkCore;
using ShopApplication.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.DataAccess.Concrete.EntityFramework
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T> // IRepository'den inherit ettik ayrıca bazı kısıtlar getirdik.
        where T : class // T bir sınıf olmalı
        where TContext : DbContext, new() // TContext bir DbContext olmalı ve newlenebilmeli.
    {
        public void Create(T entity)
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter=null)
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                return filter == null 
                    ? context.Set<T>().ToList()
                    : context.Set<T>().Where(filter).ToList();
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                return context.Set<T>().Where(filter).SingleOrDefault();
            }
        }

        public T GetById(int id)
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Update(T entity) // Update'i override edebilmek için virtual method yaptık.
        {
            //throw new NotImplementedException();

            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
