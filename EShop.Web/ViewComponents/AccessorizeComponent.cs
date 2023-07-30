using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class AccessorizeComponent : ViewComponent
    {
        IProductService _productService;
        public AccessorizeComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {


            return await Task.FromResult(View("Accessorize", _productService.GetListProductAccessories(id)));
        }
    }
}