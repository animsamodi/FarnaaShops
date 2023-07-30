using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class OtherPageBannerComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public OtherPageBannerComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult(View("OtherPageBanner", _bannerService.GetListBannerOtherPage()));
        }
    }
}