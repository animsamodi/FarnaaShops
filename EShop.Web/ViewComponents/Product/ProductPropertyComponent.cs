using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents.Product
{
    public class ProductPropertyComponent : ViewComponent
    {
        IProductService _productService;
        public ProductPropertyComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long productId)
        {
            return await Task.FromResult(View("ProductProperty", _productService.GetProperty(productId)));
        }
    }
}
