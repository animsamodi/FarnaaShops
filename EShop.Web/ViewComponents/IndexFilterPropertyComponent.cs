using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class IndexFilterPropertyComponent : ViewComponent
    {
        private IFilterPropertyService _filterPropertyService;

        public IndexFilterPropertyComponent(IFilterPropertyService filterPropertyService)
        {
            _filterPropertyService = filterPropertyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("IndexFilterProperty", _filterPropertyService.GetListForUser()));
        }
    }
}