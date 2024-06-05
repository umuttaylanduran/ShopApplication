using Microsoft.AspNetCore.Mvc;
using ShopApplication.Business.Abstract;

namespace ShopApplication.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService; // Business katmanı Dependency Injection

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        public IActionResult Index2()
        {
            return View();
        }
    }
}
