using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class OtherPageProductDiscountComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public OtherPageProductDiscountComponent(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("OtherPageProductDiscount", _productService.GetListDiscountProducts()));
        }
    }
}