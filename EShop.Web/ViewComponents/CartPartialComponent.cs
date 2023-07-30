using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class CartPartialComponent : ViewComponent
    {

        ICartService _cartService;
        public CartPartialComponent(ICartService cartService)
        {
            _cartService = cartService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        
        {
            List<CartPageViewModel> cart = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = User as ClaimsPrincipal;
                var userid = int.Parse(user.FindFirst("userid").Value);
                cart = _cartService.GetCartDetailForCartPageByUserId(userid);
            }
            else
            {
                if (Request.Cookies["Eshopcartcookie"] != null)
                {
                    var cookie = Request.Cookies["Eshopcartcookie"];
                    cart = _cartService.GetCartDetailForCartPageByCookie(cookie);
                }
            }

            return await Task.FromResult(View("CartPartial", cart));
        }
    }
}
