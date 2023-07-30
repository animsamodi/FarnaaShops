using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class SimilarProductsComponent : ViewComponent
    {
        IProductService _productService;
        public SimilarProductsComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {


            return await Task.FromResult(View("SimilarProducts", _productService.GetListSimilarProduct(id)));
        }
    }
}