using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class MainCategoryWebComponent : ViewComponent
    {
        private ICategoryMainService _categoryMainService;

        public MainCategoryWebComponent(ICategoryMainService categoryMainService)
        {
            _categoryMainService = categoryMainService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("MainCategoryWeb", _categoryMainService.GetListForUser()));
        }


    }
}