using System;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class TopHeaderBannerComponent : ViewComponent
    {
        private ISiteSettingService _siteSettingService;
        private IColleaugeSettingService     _colleaugeSettingService;

        public TopHeaderBannerComponent(ISiteSettingService siteSettingService, IColleaugeSettingService colleaugeSettingService)
        {
            _siteSettingService = siteSettingService;
            _colleaugeSettingService = colleaugeSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isColleauge = false)
        {
            TopHeaderViewModel value = new TopHeaderViewModel();
            if (isColleauge)
            {
                value = _colleaugeSettingService.GetTopHeader();
          }
            else
            {     value = _siteSettingService.GetTopHeader();
  }
              
            return await Task.FromResult(View("TopHeaderBanner", value));
        }
    }
}