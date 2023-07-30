using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class FooterMenuComponent : ViewComponent
    {
        private readonly ISiteFotterMenuService _siteFotterMenuService;
        IUserService _userService;

        public FooterMenuComponent( IUserService userService, ISiteFotterMenuService siteFotterMenuService)
        {
            _userService = userService;
            _siteFotterMenuService = siteFotterMenuService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            TempData["IsColleague"] = _userService.IsUserColleague();
            string Key = "FooterMenuChache";
            List<SiteFotterMenu> value = new List<SiteFotterMenu>();

          
                value = _siteFotterMenuService.GetListForUser();



            return await Task.FromResult(View("FooterMenu", value));
        }


    }
}