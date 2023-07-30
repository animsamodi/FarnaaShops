using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EShop.DataLayer.Enum;

namespace EShop.Web.ViewComponents
{
    public class SliderComponent:ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
         }

        public async Task<IViewComponentResult> InvokeAsync(EnumTypeSlider type)
        {
            return await Task.FromResult(View("HomeSlider", _sliderService.GetAllSlider(type)));
        }


    }
}
