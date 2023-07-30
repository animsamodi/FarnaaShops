using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class RelatedProductsComponent : ViewComponent
    {
        IProductService _productService;
        public RelatedProductsComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var relatedes = _productService.GetListRelatedProduct(id);

            return await Task.FromResult(View("RelatedProducts", relatedes));
        }
    }
}
