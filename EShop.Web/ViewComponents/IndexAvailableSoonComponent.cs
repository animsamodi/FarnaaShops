using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexAvailableSoonComponent : ViewComponent
    {
        private IProductService _productService;

        public IndexAvailableSoonComponent(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexAvailableSoon", _productService.GetListAvailableSoonProducts()));
        }
    }
}