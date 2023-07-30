using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class MainMenuComponent : ViewComponent
    {

        private IMainMenuService _mainMenuService;
        private ICategoryMainService _categoryMainService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;

        public MainMenuComponent(IMainMenuService mainMenuService, ICategoryService categoryService
            , ICategoryMainService categoryMainService, IBrandService brandService)
        {
            _mainMenuService = mainMenuService;
            _categoryMainService = categoryMainService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MainMenuShowViewModel> value = new List<MainMenuShowViewModel>();
     
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
                            haveChild = subcategory.Any(c => c.ParentId == item.Id)
                            ,
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
            return await Task.FromResult(View("MainMenu", new Tuple<List<MainMenuShowViewModel>, List<CategotyMain>, 
                List<SearchCategoryViewModel>, Dictionary<long, List<Brand>>>(value, mainCategories, categories,brands)));
        }
    }
}
