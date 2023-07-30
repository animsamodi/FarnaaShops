using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Order
{
    public class CartService : BaseService<CartDetail>, ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CartService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        #region Cart
        public long AddCart(Cart cart)
        {
            cart = cart.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(cart);
            _context.SaveChanges();
            return cart.Id;
        }

        public long UpdateCart(Cart cart)
        {
            cart = cart.SetEditDefaultValue(_userService.GetUserId());

            _context.Update(cart);
            _context.SaveChanges();
            return cart.Id;
        }

        public Cart GetCartByCookie(string cookie) => _context.Carts.FirstOrDefault(c => c.Coockie == cookie);
        public Cart GetCartByUserId(int userid) => _context.Carts.FirstOrDefault(c => c.UserId == userid);
        #endregion


        #region CartDetial
        public bool AddCartDetail(CartDetail cartDetail)
        {
            cartDetail = cartDetail.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(cartDetail);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateCartDetail(CartDetail cartDetail)
        {
            cartDetail = cartDetail.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(cartDetail);
            _context.SaveChanges();
            return true;
        }

        public CartDetail GetCartDetailByCartIdAndVariantId(long cartId, int variantId)
        {
            return _context.CartDetails.FirstOrDefault(c => c.CartId == cartId && c.VariantId == variantId);
        }

        public List<CartPageViewModel> GetCartDetailForCartPageByUserId(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();

                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                             join cd in _context.CartDetails on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join g in _context.Guarantees on v.GuaranteeId equals g.Id
                             join s in _context.Sellers on v.SellerId equals s.Id
                             join pp in _context.ProductOptions on v.ProductOptionId equals pp.Id
                             join p in _context.Products on v.ProductId equals p.Id
                             join cat in _context.Categories on p.CategoryId equals cat.Id
                             join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                              on v.Id equals vp.VariantId into variantpromotion
                             from vp in variantpromotion.DefaultIfEmpty()
                             select new CartPageViewModel
                             {
                                 ProductId = p.Id,
                                 CartCount = cd.Count,
                                 IsActiveCart = cd.IsActiveCart,
                                 CartDetialId = cd.Id,
                                 CartId = c.Id,
                                 CartPrice = cd.Price,
                                 Guarantee = g.Title,
                                 SellerName = s.Name,
                                 ProductOption = pp.Name,
                                 ProductOptionValue = pp.Value,
                                 ImgName = p.ImgName,
                                 ProductName = p.FaTitle,
                                 EnTitle = p.EnTitle ?? p.FaTitle,
                                 VariantId = v.Id,
                                 Weight = p.Weight,
                                 CategoryName = p.Category.EnTitle,
                                 MainPrice = isUserColleague ? v.PriceColleague : v.Price,
                                 SpecialPrice = isUserColleague ? v.PriceColleague : vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleague : (int)(vp != null ? vp.MaxOrderCount : v.MaxOrderCount),
                                 PromotionType = (byte)(vp != null ? vp.Type : 3),
                                 RemianingCount = isUserColleague ? v.Count : vp != null ? vp.Count : v.ShopCount > 0 ? v.ShopCount : v.Count

                             }).AsNoTracking().ToList();
                return query;
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();

                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                             join cd in _context.CartDetails on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join g in _context.Guarantees on v.GuaranteeId equals g.Id
                             join s in _context.Sellers on v.SellerId equals s.Id
                             join pp in _context.ProductOptions on v.ProductOptionId equals pp.Id
                             join p in _context.Products on v.ProductId equals p.Id
                             join cat in _context.Categories on p.CategoryId equals cat.Id
                            // join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                            //  on v.Id equals vp.VariantId into variantpromotion
                            // from vp in variantpromotion.DefaultIfEmpty()
                             select new CartPageViewModel
                             {
                                 ProductId = p.Id,
                                 CartCount = cd.Count,
                                 IsActiveCart = cd.IsActiveCart,
                                 CartDetialId = cd.Id,
                                 CartId = c.Id,
                                 CartPrice = cd.Price,
                                 Guarantee = g.Title,
                                 SellerName = s.Name,
                                 ProductOption = pp.Name,
                                 ProductOptionValue = pp.Value,
                                 ImgName = p.ImgName,
                                 ProductName = p.FaTitle,
                                 EnTitle = p.EnTitle ?? p.FaTitle,
                                 VariantId = v.Id,
                                 Weight = p.Weight,
                                 CategoryName = p.Category.EnTitle,
                                 MainPrice = isUserColleague ? v.PriceColleaguePlus : v.PricePlus,
                                 SpecialPrice = isUserColleague ? v.PriceColleaguePlus /*: vp != null ? vp.Price*/ : v.SepcialPlusPrice > 0 ? v.SepcialPlusPrice : v.PricePlus,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleaguePlus : /*(int)(vp != null ? vp.MaxOrderCount :*/ v.MaxOrderCountPlus/*)*/,
                                 //PromotionType = (byte)(vp != null ? vp.Type : 3),
                                 RemianingCount = isUserColleague ? v.Count : /*vp != null ? vp.Count :*/ v.ShopCount > 0 ? v.ShopCount : v.Count

                             }).AsNoTracking().ToList();
                return query;
            }

        }
        public List<CartPageViewModel> GetCartDetailForCartPageByCookie(string cookie, EnumTypeSystem typeSystem)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();

                var query = (from c in _context.Carts.Where(c => c.Coockie == cookie)
                             join cd in _context.CartDetails on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join g in _context.Guarantees on v.GuaranteeId equals g.Id
                             join s in _context.Sellers on v.SellerId equals s.Id
                             join pp in _context.ProductOptions on v.ProductOptionId equals pp.Id
                             join p in _context.Products on v.ProductId equals p.Id
                             join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                              on v.Id equals vp.VariantId into variantpromotion
                             from vp in variantpromotion.DefaultIfEmpty()
                             select new CartPageViewModel
                             {
                                 ProductId = p.Id,
                                 CartCount = cd.Count,
                                 CartDetialId = cd.Id,
                                 CartId = c.Id,
                                 CartPrice = cd.Price,
                                 Guarantee = g.Title,
                                 SellerName = s.Name,
                                 ProductOption = pp.Name,
                                 ProductOptionValue = pp.Value,
                                 ImgName = p.ImgName,
                                 ProductName = p.FaTitle,
                                 EnTitle = p.EnTitle ?? p.FaTitle,
                                 VariantId = v.Id,
                                 Weight = p.Weight,
                                 MainPrice = isUserColleague ? v.PriceColleague : v.Price,
                                 SpecialPrice = isUserColleague ? v.PriceColleague : vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,

                                 IsActiveCart = cd.IsActiveCart,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleague : (int)(vp != null ? vp.MaxOrderCount : v.MaxOrderCount),
                                 PromotionType = (byte)(vp != null ? vp.Type : 3),
                                 RemianingCount = vp != null ? vp.Count : v.ShopCount > 0 ? v.ShopCount : v.Count

                             }).AsNoTracking().ToList();
                return query;
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();

                var query = (from c in _context.Carts.Where(c => c.Coockie == cookie)
                             join cd in _context.CartDetails on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join g in _context.Guarantees on v.GuaranteeId equals g.Id
                             join s in _context.Sellers on v.SellerId equals s.Id
                             join pp in _context.ProductOptions on v.ProductOptionId equals pp.Id
                             join p in _context.Products on v.ProductId equals p.Id
                           //  join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                            //  on v.Id equals vp.VariantId into variantpromotion
                            // from vp in variantpromotion.DefaultIfEmpty()
                             select new CartPageViewModel
                             {
                                 ProductId = p.Id,
                                 CartCount = cd.Count,
                                 CartDetialId = cd.Id,
                                 CartId = c.Id,
                                 CartPrice = cd.Price,
                                 Guarantee = g.Title,
                                 SellerName = s.Name,
                                 ProductOption = pp.Name,
                                 ProductOptionValue = pp.Value,
                                 ImgName = p.ImgName,
                                 ProductName = p.FaTitle,
                                 EnTitle = p.EnTitle ?? p.FaTitle,
                                 VariantId = v.Id,
                                 Weight = p.Weight,
                                 MainPrice = isUserColleague ? v.PriceColleaguePlus : v.PricePlus,
                                 SpecialPrice = isUserColleague ? v.PriceColleaguePlus /*: vp != null ? vp.Price*/ : v.SepcialPlusPrice > 0 ? v.SepcialPlusPrice : v.PricePlus,

                                 IsActiveCart = cd.IsActiveCart,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleaguePlus /*: (int)(vp != null ? vp.MaxOrderCount*/ : v.MaxOrderCountPlus/*)*/,
                                 //PromotionType = (byte)(vp != null ? vp.Type : 3),
                                 RemianingCount =/* vp != null ? vp.Count :*/ v.ShopCount > 0 ? v.ShopCount : v.Count

                             }).AsNoTracking().ToList();
                return query;
            }
        
        }

        public void ListUpdateCartDetial(List<CartDetail> cartDetails)
        {
            foreach (var cartDetail in cartDetails)
            {
                cartDetail.LastUpdateDate = DateTime.Now;

            }
            _context.UpdateRange(cartDetails);
            _context.SaveChanges();
        }

        public void ListDeleteCartDetial(List<CartDetail> cartDetails)
        {
            List<CartDetail> details = new List<CartDetail>();
            foreach (var cartDetail in cartDetails)
            {
                var det = _context.CartDetails.Find(cartDetail.Id);
                det.LastUpdateDate = DateTime.Now;
                det.IsDelete = true;
                details.Add(det);
            }
            _context.UpdateRange(details);
            _context.SaveChanges();
        }

        public List<ShippingPageProduct> GetCartDetialForShippingPage(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();
                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                    join cd in _context.CartDetails on c.Id equals cd.CartId
                    join v in _context.Variants on cd.VariantId equals v.Id
                    join g in _context.Guarantees on v.GuaranteeId equals g.Id
                    join op in _context.ProductOptions on v.ProductOptionId equals op.Id

                    join s in _context.Sellers on v.SellerId equals s.Id

                    join p in _context.Products on v.ProductId equals p.Id
                    join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                        on v.Id equals vp.VariantId into variantpromotion
                    from vp in variantpromotion.DefaultIfEmpty()
                    select new ShippingPageProduct
                    {
                        ProductName = p.FaTitle,
                        ImagName = p.ImgName,

                        CartCount = cd.Count,
                        MainPrice = isUserColleague ? v.PriceColleague :
                            v.Price,
                        SpecialPrice = isUserColleague ? v.PriceColleague :
                            vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                        Guarantee = g.Title,
                        ProductOption = op.Name,
                        ProductOptionValue = op.Value
                    }).ToList();
                return query;
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();
                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                    join cd in _context.CartDetails on c.Id equals cd.CartId
                    join v in _context.Variants on cd.VariantId equals v.Id
                    join g in _context.Guarantees on v.GuaranteeId equals g.Id
                    join op in _context.ProductOptions on v.ProductOptionId equals op.Id

                    join s in _context.Sellers on v.SellerId equals s.Id

                    join p in _context.Products on v.ProductId equals p.Id
                    //join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                    //    on v.Id equals vp.VariantId into variantpromotion
                    //from vp in variantpromotion.DefaultIfEmpty()
                    select new ShippingPageProduct
                    {
                        ProductName = p.FaTitle,
                        ImagName = p.ImgName,

                        CartCount = cd.Count,
                        MainPrice = isUserColleague ? v.PriceColleaguePlus :
                            v.PricePlus,
                        SpecialPrice = isUserColleague ? v.PriceColleaguePlus :
                            /*vp != null ? vp.Price :*/ v.SepcialPlusPrice > 0 ? v.SepcialPlusPrice : v.PricePlus,
                        Guarantee = g.Title,
                        ProductOption = op.Name,
                        ProductOptionValue = op.Value
                    }).ToList();
                return query;
            }
    
        }

        public List<CartDetailForDiscountViewModel> GetCartDetailForCheckDiscountCode(int userid)

        {
            var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                         join cd in _context.CartDetails on c.Id equals cd.CartId
                         join v in _context.Variants on cd.VariantId equals v.Id
                         join p in _context.Products on v.ProductId equals p.Id
                         join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                         on v.Id equals vp.VariantId into variantpromotion
                         from vp in variantpromotion.DefaultIfEmpty()
                         select new CartDetailForDiscountViewModel
                         {
                             CategoryId = (from category in p.ProductCategories select category.CategoryId).ToList(),
                             Count = cd.Count,
                             MainPrice = v.Price,
                             SpecialPrice = vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                         }).ToList();
            return query;
        }

        public List<CartDetailForSubmitViewModel> GetCartDetialForSubmitOrder(int userid, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleagueByUserId(userid);

                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                             join cd in _context.CartDetails.Where(c => c.IsActiveCart) on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join s in _context.Sellers on v.SellerId equals s.Id

                             join p in _context.Products on v.ProductId equals p.Id
                             join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                              on v.Id equals vp.VariantId into variantpromotion
                             from vp in variantpromotion.DefaultIfEmpty()
                             select new CartDetailForSubmitViewModel
                             {
                                 CartDetailId = cd.Id,
                                 CategoryId = (from category in p.ProductCategories select category.CategoryId).ToList(),
                                 CartPrice = cd.Price,
                                 VariantId = cd.VariantId,
                                 Weight = p.Weight,
                                 Dely = v.ShopCount > 0 ? 0 : s.DeliveryTime,
                                 CartCount = cd.Count,
                                 MainPrice = isUserColleague ? v.PriceColleague : v.Price,
                                 SpecialPrice = isUserColleague ? v.PriceColleague : vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                                 RemianingCount = isUserColleague ? v.Count : vp != null ? vp.Count : v.ShopCount > 0 ? v.ShopCount : v.Count,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleague : (int)(vp != null ? vp.MaxOrderCount : v.MaxOrderCount),
                                 VariantPromotion = vp,
                                 variant = v,
                                 PromotionType = (byte)(vp != null ? vp.Type : 3),
                                 ProductSpecCode = p.SpecCode

                             }).ToList();
                return query;
            }
            else
            {
                var isUserColleague = _userService.IsUserColleagueByUserId(userid);

                var query = (from c in _context.Carts.Where(c => c.UserId == userid)
                             join cd in _context.CartDetails.Where(c => c.IsActiveCart) on c.Id equals cd.CartId
                             join v in _context.Variants on cd.VariantId equals v.Id
                             join s in _context.Sellers on v.SellerId equals s.Id

                             join p in _context.Products on v.ProductId equals p.Id
                             //join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                             // on v.Id equals vp.VariantId into variantpromotion
                             //from vp in variantpromotion.DefaultIfEmpty()
                             select new CartDetailForSubmitViewModel
                             {
                                 CartDetailId = cd.Id,
                                 CategoryId = (from category in p.ProductCategories select category.CategoryId).ToList(),
                                 CartPrice = cd.Price,
                                 VariantId = cd.VariantId,
                                 Weight = p.Weight,
                                 Dely = v.ShopCount > 0 ? 0 : s.DeliveryTime,
                                 CartCount = cd.Count,
                                 MainPrice = isUserColleague ? v.PriceColleaguePlus : v.PricePlus,
                                 SpecialPrice = isUserColleague ? v.PriceColleaguePlus /*: vp != null ? vp.Price*/ : v.SepcialPlusPrice > 0 ? v.SepcialPlusPrice : v.PricePlus,
                                 RemianingCount = isUserColleague ? v.Count /*: vp != null ? vp.Count*/ : v.ShopCount > 0 ? v.ShopCount : v.Count,
                                 MaxOrderCount = isUserColleague ? v.MaxOrderCountColleaguePlus : (int)(/*vp != null ? vp.MaxOrderCount :*/ v.MaxOrderCountPlus),
                                 //VariantPromotion = vp,
                                 variant = v,
                                 PromotionType = 3,//(byte)(vp != null ? vp.Type : 3),
                                 ProductSpecCode = p.SpecCode

                             }).ToList();
                return query;

            }
    
        }

        public void RemoveCartDetialList(List<CartDetail> cartDetails)
        {
            List<CartDetail> details = new List<CartDetail>();
            foreach (var cartDetail in cartDetails)
            {
                var cart = _context.CartDetails.Find(cartDetail.Id);
                cart.SetRemoveDefaultValue(_userService.GetUserId());
                details.Add(cart);
            }
            _context.UpdateRange(details);
            _context.SaveChanges();
        }

        public List<CartDetail> GetNotActiveCartDetail(int userid)
        {
            return _context.CartDetails.Where(c => c.Cart.UserId == userid).ToList();
        }

        public void RemoveCartDetail(CartDetail cartDetail)
        {
            cartDetail.SetRemoveDefaultValue(_userService.GetUserId());
            _context.Update(cartDetail);
            _context.SaveChanges();
        }

        public Shipment GetShipmentById(long id)
        {
            return _context.Shipments.Find(id);

        }

        public bool UpdateCartShipment(long userid, long shippingId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserId == userid);
                cart.ShipmentId = shippingId;
                _context.Update(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCartAddress(long userid, long addressId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserId == userid);
                cart.AddressId = addressId;
                _context.Update(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveCartAndCartDetail(Cart userCart)
        {
          
            try
            {
                var details = _context.CartDetails.Where(c => c.CartId == userCart.Id).ToList();
                RemoveCartDetialList(details);
                userCart.SetRemoveDefaultValue(_userService.GetUserId());
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCartPacking(int userid, int packingId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.UserId == userid);
                cart.PackingId = packingId;
                _context.Update(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

    }
}
