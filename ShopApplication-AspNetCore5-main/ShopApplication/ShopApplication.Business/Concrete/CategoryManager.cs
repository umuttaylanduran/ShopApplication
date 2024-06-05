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
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal; // DataAccess katmanına erişmek için Dependency Injection

        public CategoryManager(ICategoryDal categoryDal) // constructor
        {
            _categoryDal = categoryDal;
        }



        public void Create(Category entity)
        {
            //throw new NotImplementedException();
            _categoryDal.Create(entity);
        }

        public void Delete(Category entity)
        {
            //throw new NotImplementedException();
            _categoryDal.Delete(entity);
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            //throw new NotImplementedException();
            _categoryDal.DeleteFromCategory(categoryId, productId);
        }

        public List<Category> GetAll()
        {
            //throw new NotImplementedException();

            return _categoryDal.GetAll();   
        }

        public Category GetById(int id)
        {
            //throw new NotImplementedException();
            return _categoryDal.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
            //throw new NotImplementedException();
            return _categoryDal.GetByIdWithProducts(id);
        }

        public void Update(Category entity)
        {
            //throw new NotImplementedException();
            _categoryDal.Update(entity);
        }
    }
}
