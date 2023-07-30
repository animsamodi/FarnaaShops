using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ColleaugeSpecialSaleProductComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ColleaugeSpecialSaleProductComponent(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("ColleaugeSpecialSaleProduct", _productService.GetColleaugeSpecialSaleProduct()));
        }
    }
}