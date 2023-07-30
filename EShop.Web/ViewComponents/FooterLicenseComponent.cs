using System;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Site;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class FooterLicenseComponent : ViewComponent
    {
        private ISiteFotterLinkService _siteFotterLinkService;

        public FooterLicenseComponent(ISiteFotterLinkService siteFotterLinkService)
        {
            _siteFotterLinkService = siteFotterLinkService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SiteFotterLink> value = new List<SiteFotterLink>();
         
                value = _siteFotterLinkService.GetSiteListFooterLicense();

            return await Task.FromResult(View("FooterLicense", value));
        }
    }
}