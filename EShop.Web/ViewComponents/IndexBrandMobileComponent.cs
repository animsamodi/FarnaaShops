using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexBrandMobileComponent : ViewComponent
    {
        private IBrandService _brandService;

        public IndexBrandMobileComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexBrandMobile", _brandService.GetListUserShowBrand()));
        }
    }
}