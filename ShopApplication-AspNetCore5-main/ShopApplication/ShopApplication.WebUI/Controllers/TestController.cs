using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.WebUI.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
