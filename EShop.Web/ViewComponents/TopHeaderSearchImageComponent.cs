using System;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class TopHeaderSearchImageComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public TopHeaderSearchImageComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            BannerImageViewModel value = new BannerImageViewModel();
      
                value = _bannerService.GetSearchBanner();
            

            return await Task.FromResult(View("TopHeaderSearchImage", value));
        }
    }
}