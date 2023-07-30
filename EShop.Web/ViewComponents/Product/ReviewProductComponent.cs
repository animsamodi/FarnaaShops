using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents.Product
{
    public class ReviewProductComponent : ViewComponent
    {
        readonly IProductService _productService;
        public ReviewProductComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long productId)
        {
            return await Task.FromResult(View("ReviewProduct", _productService.GetProductReview(productId)));
        }

    }
}
