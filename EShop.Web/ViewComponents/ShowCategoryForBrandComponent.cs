using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ShowCategoryForBrandComponent:ViewComponent
    {
        readonly ICategoryService _categoryService;
        public ShowCategoryForBrandComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            return await Task.FromResult(View("ShowCategoryForBrand", _categoryService.GetCategoryByBrandId(id)));
        }
    }
}
