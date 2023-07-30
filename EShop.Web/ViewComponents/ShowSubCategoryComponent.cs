using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ShowSubCategoryComponent : ViewComponent
    {
        ICategoryService _categoryservice;
        public ShowSubCategoryComponent(ICategoryService categoryService)
        {
            _categoryservice = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult(View("ShowsubCategory", _categoryservice.GetSubCategoryById(id)));
        }
    }
}
