using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexFilterPriceComponent : ViewComponent
    {
        private IFilterPriceService _filterPriceService;

        public IndexFilterPriceComponent(IFilterPriceService filterPriceService)
        {
            _filterPriceService = filterPriceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexFilterPrice", _filterPriceService.GetListForUser()));
        }
    }
}
