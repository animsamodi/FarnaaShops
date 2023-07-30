using EShop.Core.Services.Interfaces.Components;
using EShop.Core.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.ViewComponents
{
    public class CircleComponentsCarouselComponent : ViewComponent
    {

        IUiCircleComponentService _circleComponentService;
        public CircleComponentsCarouselComponent(IUiCircleComponentService circleComponentService)
        {
            _circleComponentService = circleComponentService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var components = await _circleComponentService.GetAll();
            var models = components.Select(c => new CircleComponentViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Url = c.Url,
            }).ToList();
            return await Task.FromResult(View("CircleComponentsCarousel", models));
        }
    }

}
