using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class ColleaugeCreditComponent : ViewComponent
    {

        IUserService _userService;
        public ColleaugeCreditComponent(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
           
                var user = User as ClaimsPrincipal;
                var userid = int.Parse(user.FindFirst("userid").Value);
                var res = _userService.GetUserById(userid);
                
          

            return await Task.FromResult(View("ColleaugeCredit", res));
        }
    }
}
