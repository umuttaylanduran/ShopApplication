using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopApplication.Business.Abstract;
using ShopApplication.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.UI.Controllers
{
    public class HomeController : Controller
    {
        // Dependency Injection
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService; 

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll(),
                //Categories = _categoryService.GetAll(),
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
