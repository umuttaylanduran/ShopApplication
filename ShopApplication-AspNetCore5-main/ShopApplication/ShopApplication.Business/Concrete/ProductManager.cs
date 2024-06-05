using ShopApplication.Business.Abstract;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.DataAccess.Concrete.EntityFramework;
using ShopApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Business.Concrete
{
    public class ProductManager : IProductService
    {
        //EfCoreProductDal _productDal = new EfCoreProductDal(); // Bunu kullanmam yanlış. Alt satırdaki gibi bir Injection işlemi yapmalıyım.

        private IProductDal _productDal; // DataAccess katmanına erişmek için Dependency Injection

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public bool Create(Product entity)
        {
            if (Validate(entity))
            {
                _productDal.Create(entity);
                return true;
            }

            return false;
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            //throw new NotImplementedException();

            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            //throw new NotImplementedException();

            return _productDal.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            //throw new NotImplementedException();
            return _productDal.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public List<Product> GetProductByCategory(string category, int page, int pageSize)
        {
            //throw new NotImplementedException();
            return _productDal.GetProductsByCategory(category, page, pageSize);
        }

        public Product GetProductDetails(int id)
        {
            //throw new NotImplementedException();
            return _productDal.GetProductDetails(id);
        }

        public void Update(Product entity)
        {
            //throw new NotImplementedException();
            _productDal.Update(entity);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            //throw new NotImplementedException();
            _productDal.Update(entity, categoryIds);
        }

        public bool Validate(Product entity)
        {
            //throw new NotImplementedException();
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün ismi girmelisiniz.";
                isValid = false;
            }

            

            return isValid;
        }
        public string ErrorMessage { get; set; }
    }
}
