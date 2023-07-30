using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class BestViewProductComponent : ViewComponent
    {
        IProductService _productService;
        public BestViewProductComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("BestViewProduct", _productService.GetProductBestView()));
        }
    }
}
