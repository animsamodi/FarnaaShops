using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class MenuHeaderComponent : ViewComponent
    {
        private ISiteMenuService _siteMenuService;

        public MenuHeaderComponent(ISiteMenuService siteMenuService)
        {
            _siteMenuService = siteMenuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SiteMenu> value = new List<SiteMenu>();
           
                value = _siteMenuService.GetListForUser();

            return await Task.FromResult(View("MenuHeader", value));
        }
    }
}