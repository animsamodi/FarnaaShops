using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ICartService : IBaseService<CartDetail>
    {
        #region Cart
        long AddCart(Cart cart);
        long UpdateCart(Cart cart);
         Cart GetCartByCookie(string cookie);
        Cart GetCartByUserId(int userid);
        #endregion

        #region Cartdetail
        bool AddCartDetail(CartDetail cartDetail);
        bool UpdateCartDetail(CartDetail cartDetail);
        CartDetail GetCartDetailByCartIdAndVariantId(long cartid, int variantd);
        List<CartPageViewModel> GetCartDetailForCartPageByCookie(string cookie, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<CartPageViewModel> GetCartDetailForCartPageByUserId(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        void ListUpdateCartDetial(List<CartDetail> cartDetails);
        void ListDeleteCartDetial(List<CartDetail> cartDetails);
        List<ShippingPageProduct> GetCartDetialForShippingPage(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<CartDetailForDiscountViewModel> GetCartDetailForCheckDiscountCode(int userid);
        List<CartDetailForSubmitViewModel> GetCartDetialForSubmitOrder(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        void RemoveCartDetialList(List<CartDetail> cartDetails);
        List<CartDetail> GetNotActiveCartDetail(int userid);
        #endregion

        void RemoveCartDetail(CartDetail cartDetail);
        Shipment GetShipmentById(long id);
        bool UpdateCartShipment(long userid, long shippingId);
        bool UpdateCartAddress(long userid,long addressId);
        bool RemoveCartAndCartDetail(Cart userCart);
        bool UpdateCartPacking(int userid, int packingId);
    }
}
