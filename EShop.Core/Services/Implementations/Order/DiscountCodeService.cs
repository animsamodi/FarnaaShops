using System.Linq;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Cart;

namespace EShop.Core.Services.Implementations.Order
{
   public class DiscountCodeService : BaseService<DiscountCode>, IDiscountCodeService
   {
       private readonly ApplicationDbContext _context;
       private readonly IUserService _userService;

        public DiscountCodeService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


       public DiscountCode GetDiscountCode(string code)
        {
            return _context.DiscountCodes.FirstOrDefault(d => d.Code == code);

        }
    }
}
