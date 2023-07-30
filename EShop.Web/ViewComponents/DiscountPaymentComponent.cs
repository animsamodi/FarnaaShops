using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class DiscountPaymentComponent : ViewComponent
    {
        IGiftCartService _giftCartService;
        public DiscountPaymentComponent(IGiftCartService giftCartService)
        {
            _giftCartService = giftCartService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
           
                var user = User as ClaimsPrincipal;
                var userid = int.Parse(user.FindFirst("userid").Value);
                
           

            return await Task.FromResult(View("DiscountPayment", _giftCartService.GetGiftCartsByUserId(userid)));
        }
    }
}
