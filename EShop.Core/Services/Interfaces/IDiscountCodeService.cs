using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Cart;

namespace EShop.Core.Services.Interfaces
{
    public interface IDiscountCodeService : IBaseService<DiscountCode>
    {
        DiscountCode GetDiscountCode(string code);
    }
}
