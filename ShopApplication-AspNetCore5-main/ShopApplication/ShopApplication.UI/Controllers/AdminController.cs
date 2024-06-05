using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.Business.Abstract;
using ShopApplication.Entities;
using ShopApplication.UI.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // Dependency Injection

        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Product Management

        public IActionResult ProductList()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost] // Post model alır.
        public IActionResult CreateProduct(ProductModel model) 
        {
            if(ModelState.IsValid==true) // validation (kısıtlamalar) uygunsa
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    //ImageUrl = model.ImageUrl
                };

                if (_productService.Create(entity))
                {
                    return Redirect("ProductList"); // işlemler tamamlanırsa --> /admin/productlist 
                }
                ViewBag.ErrorMessage = _productService.ErrorMessage;
                return View(model);
   
            }

            return View(model); // Eğer if bloğu sağlanmadıysa kullanıcıyı aynı sayfaya geri göndeririz.

          
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategories((int)id);    

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.ProductCategories.Select(c => c.Category).ToList() 
            };

            ViewBag.Categories = _categoryService.GetAll(); // GetAll  ile bütün Category bilgilerini alıyoruz.

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var entity = _productService.GetById(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                //entity.ImageUrl = model.ImageUrl; --> ImageUrl yi file olarak yüklüyoruz.
                entity.Price = model.Price;

                if (file!=null) // kullanıcı bir resim göndermiş ise.
                {
                    entity.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img",file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); // asenktron saklama
                    }
                }
                 
                _productService.Update(entity, categoryIds);

                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = _categoryService.GetAll(); // GetAll  ile bütün Category bilgilerini alıyoruz.

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);

            if (entity != null)
            {
                _productService.Delete(entity);
            }

            return RedirectToAction("ProductList");
        }

        // Category Management
 
        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

       [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost] // Post model alır.
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
            };

            _categoryService.Create(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithProducts(id);

            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            }); 
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;   
            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            return RedirectToAction("CategoryList");
        }


        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId) // Veritabanından değil, Yalnızca Category'den silme işlemi yapar.
        {
            _categoryService.DeleteFromCategory(categoryId, productId);

            return Redirect("/admin/editcategory/"+categoryId);
        }

    }
}
