using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ListBrandComponent : ViewComponent
    {
        private readonly IBrandService _brandService;

        public ListBrandComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("ListBrand", _brandService.GetListBrand()));
        }
    }
}
