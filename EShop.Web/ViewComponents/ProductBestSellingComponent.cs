using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ProductBestSellingComponent : ViewComponent
    {
        IProductService _productService;
        public ProductBestSellingComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("ProductBestSelling", _productService.GetProductBestSelling()));
        }
    }
}
