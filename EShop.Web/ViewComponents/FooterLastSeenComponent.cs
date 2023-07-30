using System;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EShop.DataLayer.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class FooterLastSeenComponent : ViewComponent
    {
        private IUserProductViewService _userProductViewService;

        public FooterLastSeenComponent(IUserProductViewService userProductViewService)
        {
            _userProductViewService = userProductViewService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            List<UserProductView> productViews = new List<UserProductView>();
            string Key = "FooterLastSeenChache";
            string Keycookie = "FooterLastSeencookieChache";
          
            if (User.Identity.IsAuthenticated)
            {
              
                    var user = User as ClaimsPrincipal;
                    var userid = int.Parse(user.FindFirst("userid").Value);
                    productViews = _userProductViewService.GetUserProductViewByUserId(userid).Distinct().ToList();

             
               
            }
            else
            {
                if (Request.Cookies["EshopViewcookie"] != null)
                {
                   
                        var cookie = Request.Cookies["EshopViewcookie"];
                        productViews = _userProductViewService.GetUserProductViewByCookie(cookie).Distinct().ToList();

                    
                
                }
            }

            return await Task.FromResult(View("FooterLastSeen", productViews));
        }
    }
}