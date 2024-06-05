using Microsoft.AspNetCore.Mvc;
using ShopApplication.Business.Abstract;
using ShopApplication.UI.Models;

namespace ShopApplication.UI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService) // constructor
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel()
            {
                SelectedCategory = RouteData.Values["category"]?.ToString(), // ? --> Null mu değil mi? , Eğer NULL değer ise kodun devamı çalışmaz.
                Categories = _categoryService.GetAll()
            });
        }

    }
}
