using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexBrandComponent : ViewComponent
    {
        private IBrandService _brandService;

        public IndexBrandComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexBrand", _brandService.GetListUserShowBrand()));
        }
    }
}