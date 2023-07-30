using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Cart;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class MainMenuMobileComponent : ViewComponent
    {

        private IMainMenuService _mainMenuService;
        private ICartService _cartService;
        private ICategoryMainService _categoryMainService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        public MainMenuMobileComponent(IMainMenuService mainMenuService, ICartService cartService,
            ICategoryMainService categoryMainService, ICategoryService categoryService, IBrandService brandService)
        {
            _mainMenuService = mainMenuService;
            _categoryMainService = categoryMainService;
            _categoryService = categoryService;
            _brandService = brandService;
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MainMenuShowViewModel> value = new List<MainMenuShowViewModel>();
            string Key = "MainmenuChache";
            
                value = _mainMenuService.GetMainMenu();
            
            var mainCategories = _categoryMainService.GetListForUser();

            var brands = new Dictionary<long, List<Brand>>();
            foreach (var item in mainCategories)
                if (item.BrandId != 0)
                    brands.Add(item.CategoryId.Value, _brandService.GetBrandByCategoryId(item.CategoryId.Value));

            List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            var subcategory = _categoryService.GetAllSubCategory();
            var category = _categoryService.GetAllCategory().Where(c => c.Id != 29).ToList();

            foreach (var item in category)
            {
                var subid = subcategory.Where(c => c.SubId == item.Id);

                if (subid.Count() > 0)
                {
                    foreach (var item2 in subid)
                    {
                        categories.Add(new SearchCategoryViewModel
                        {
                            CategoryId = item.Id,
                            ParentId = item2.ParentId,
                            Title = item.FaTitle,
                            haveChild = subcategory.Any(c => c.ParentId == item.Id),
                            CategoryEnTitle = item.EnTitle
                        });
                    }
                }
                else
                {
                    categories.Add(new SearchCategoryViewModel
                    {
                        CategoryId = item.Id,
                        ParentId = null,
                        Title = item.FaTitle,
                        haveChild = subcategory.Any(c => c.ParentId == item.Id),
                        CategoryEnTitle = item.EnTitle
                    });
                }
            }
            List<CartPageViewModel> cart
              = new List<CartPageViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                var user = User as ClaimsPrincipal;
                var userid = int.Parse(user.FindFirst("userid").Value);
                cart = _cartService.GetCartDetailForCartPageByUserId(userid);
            }
            else
            {
                if (Request.Cookies["Eshopcartcookie"] != null)
                {
                    var cookie = Request.Cookies["Eshopcartcookie"];
                    cart = _cartService.GetCartDetailForCartPageByCookie(cookie);
                }
            }
            return await Task.FromResult(View("MainMenuMobile", new Tuple<List<MainMenuShowViewModel>, List<CategotyMain>,int,
                List<SearchCategoryViewModel>, Dictionary<long, List<Brand>>>(value, mainCategories, cart.Sum(c => c.CartCount), categories, brands)));
        }
    }
}
