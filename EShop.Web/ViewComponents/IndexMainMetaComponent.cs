using System;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class IndexMainMetaComponent : ViewComponent
    {
        private readonly ISiteSettingService _settingService;

        public IndexMainMetaComponent(   ISiteSettingService settingService)
        {
            _settingService = settingService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            IndexMainMetaViewModel value = new IndexMainMetaViewModel();
          
                value = _settingService.GetIndexMainMetaViewModel();
               
            return await Task.FromResult(View("IndexMainMeta", value ));
        }
    }
    public class BlogMainMetaComponent : ViewComponent
    {
        private readonly ISiteSettingService _settingService;

        public BlogMainMetaComponent(   ISiteSettingService settingService)
        {
            _settingService = settingService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            IndexMainMetaViewModel value = new IndexMainMetaViewModel();
            string Key = "BlogMainMeta";
      
                value = _settingService.GetBlogMainMetaViewModel();
        
            return await Task.FromResult(View("BlogMainMeta", value ));
        }
    }
}