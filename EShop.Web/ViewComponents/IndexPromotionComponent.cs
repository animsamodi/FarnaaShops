using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexPromotionComponent : ViewComponent
    {
        IProductService _productService;
        public IndexPromotionComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexPromotion", _productService.GetProductPromotionForIndex()));
        }
    }
}
