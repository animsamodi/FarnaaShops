using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Sender;
using EShop.Core.ServiceApi;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using EShop.Web.Helper;
using EShop.Web.Models;
using EShop.Web.Models.IranKish;
using EShop.Web.ViewComponents.Cart;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EShop.Web.Models.Melli;
using System.Security.Cryptography;
using System.Net;
using StackExchange.Redis;


namespace EShop.Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IVariantService _variantService;
        private readonly IDiscountCodeService _discountCodeService;
        private readonly IOrderService _orderService;
        private readonly IGiftCartService _giftCartService;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IShipmentService _shipmentService;
        private readonly IPaymentDetialService _paymentDetialService;
        private readonly ICrmService _crmService;
        private IProductService _productService;
        private ISmsSender _smsSender;
        private ISiteSettingService _siteSettingService;
        string ZarinpalAuthority;

        public CartController(ICartService cartService, IVariantService variantService,
            IDiscountCodeService discountCodeService, IOrderService orderService, IGiftCartService giftCartService,
            IUserService userService, IAddressService addressService,
            IShipmentService shipmentService, IPaymentDetialService paymentDetialService, ICrmService crmService, ISmsSender smsSender, IProductService productService, ISiteSettingService siteSettingService)
        {
            _shipmentService = shipmentService;
            _paymentDetialService = paymentDetialService;
            _crmService = crmService;
            _smsSender = smsSender;
            _productService = productService;
            _siteSettingService = siteSettingService;
            _cartService = cartService;
            _variantService = variantService;
            _discountCodeService = discountCodeService;
            _orderService = orderService;
            _giftCartService = giftCartService;
            _userService = userService;
            _addressService = addressService;
        }
        public IActionResult Index()
        {

            return View();
        }


        public IActionResult AddToCart(int id, string extProdId = "")
        {

            AddProductToCart(id);
            if (!(string.IsNullOrWhiteSpace(extProdId)))
            {
                var prodExt = extProdId.Split(',');
                foreach (var extId in prodExt)
                {
                    if (!string.IsNullOrWhiteSpace(extId))
                        AddProductToCart(Convert.ToInt64(extId));

                }
            }

            var product = _variantService.GetProductByVariantId(id);
            TempData["SeccessAddProductToiCart"] = true;
            return RedirectToActionPermanent(nameof(Index),"Product", new { id = product.Id, name = product.EnTitle, category = product.Category.EnTitle });

            //return RedirectToAction(nameof(Index));
        }

        private void AddProductToCart(long id)
        {
            Cart cart = null;
            long cartid = 0;
            CartDetail cartDetail = null;
            if (!User.Identity.IsAuthenticated)
            {
                string cookie = "";
                if (Request.Cookies["Eshopcartcookie"] == null)
                {
                    cookie = Guid.NewGuid().ToString().Replace("-", "");
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(31)
                    };

                    Response.Cookies.Append("Eshopcartcookie", cookie, options);
                    cart = new Cart
                    {
                        Coockie = cookie,
                        UpdateDate = DateTime.Now
                    };
                    cartid = _cartService.AddCart(cart);
                }
                else
                {
                    cookie = Request.Cookies["Eshopcartcookie"];
                    cart = _cartService.GetCartByCookie(cookie);
                    if (cart != null)
                    {
                        cart.UpdateDate = DateTime.Now;
                        cartid = cart.Id;
                        _cartService.UpdateCart(cart);
                        cartDetail = _cartService.GetCartDetailByCartIdAndVariantId(cartid, (int)id);
                    }
                    else
                    {
                        cookie = Guid.NewGuid().ToString().Replace("-", "");
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(31)
                        };
                        Response.Cookies.Append("Eshopcartcookie", cookie, options);
                        cart = new Cart
                        {
                            Coockie = cookie,
                            UpdateDate = DateTime.Now
                        };
                        cartid = _cartService.AddCart(cart);
                    }
                }
            }
            else
            {
                var userid = int.Parse(User.FindFirst("userid").Value);
                cart = _cartService.GetCartByUserId(userid);
                if (cart != null)
                {
                    cart.UpdateDate = DateTime.Now;
                    cartid = cart.Id;
                    _cartService.UpdateCart(cart);
                    cartDetail = _cartService.GetCartDetailByCartIdAndVariantId(cartid, (int)id);
                }
                else
                {
                    cart = new Cart
                    {
                        UserId = userid,
                        UpdateDate = DateTime.Now
                    };
                    cartid = _cartService.AddCart(cart);
                }
            }

            var variant = _variantService.GetMinPriceAndCountByVariantId((int)id);
            if (cartDetail != null)
            {
                if (variant != null && variant.Item2 > cartDetail.Count && variant.Item3 > cartDetail.Count)
                {
                    cartDetail.Count++;
                    cartDetail.Price = variant.Item1;
                    _cartService.UpdateCartDetail(cartDetail);
                }
            }
            else
            {
                if (variant != null)
                {
                    CartDetail detail = new CartDetail
                    {
                        CartId = cartid,
                        VariantId = id,
                        Count = 1,
                        Price = variant.Item1,
                        IsActiveCart = true,
                    };

                    _cartService.AddCartDetail(detail);
                }
            }

        }
        private void AddProductToCartAfterPay(long id, long userId, int count)
        {
            Cart cart = null;
            long cartid = 0;
            CartDetail cartDetail = null;

            cart = _cartService.GetCartByUserId((int)userId);
            if (cart != null)
            {
                cart.UpdateDate = DateTime.Now;
                cartid = cart.Id;
                _cartService.UpdateCart(cart);
                cartDetail = _cartService.GetCartDetailByCartIdAndVariantId(cartid, (int)id);
            }
            else
            {
                cart = new Cart
                {
                    UserId = userId,
                    UpdateDate = DateTime.Now
                };
                cartid = _cartService.AddCart(cart);
            }


            var variant = _variantService.GetMinPriceAndCountByVariantId((int)id);
            if (cartDetail != null)
            {
                if (variant != null && variant.Item2 > cartDetail.Count && variant.Item3 > cartDetail.Count)
                {
                    cartDetail.Count++;
                    cartDetail.Price = variant.Item1;
                    _cartService.UpdateCartDetail(cartDetail);
                }
            }
            else
            {
                if (variant != null)
                {
                    CartDetail detail = new CartDetail
                    {
                        CartId = cartid,
                        VariantId = id,
                        Count = count,
                        Price = variant.Item1,
                        IsActiveCart = true,
                    };

                    _cartService.AddCartDetail(detail);
                }
            }

        }
        [HttpPost]
        public JsonResult ChangeUpdateCartDeetial(List<CartPageEditViewModel> value)
        {
            List<CartDetail> updatelist = new List<CartDetail>();
            List<CartDetail> deletelist = new List<CartDetail>();

            foreach (var item in value)
            {
                if (item.ChangeType == 2)
                    deletelist.Add(new CartDetail { Id = item.CartDetailId });

                if (item.ChangeType == 1)
                {
                    updatelist.Add(new CartDetail
                    {
                        Id = item.CartDetailId,
                        CartId = item.CartId,
                        Count = item.Count,
                        IsActiveCart = item.IsActiveCart,
                        Price = item.Price,
                        VariantId = item.VariantId
                    });
                }
            }

            _cartService.ListUpdateCartDetial(updatelist);
            _cartService.ListDeleteCartDetial(deletelist);
            return Json("ok");
        }


        [HttpPost]
        public IActionResult ChangeCartCount(int variantid, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = int.Parse(User.FindFirst("userid").Value);
                var cart = _cartService.GetCartByUserId(userid);
                if (cart != null)
                {
                    var cartdetial = _cartService.GetCartDetailByCartIdAndVariantId(cart.Id, variantid);
                    if (cartdetial != null)
                        update(cartdetial);
                }
            }
            else
            {
                if (Request.Cookies["Eshopcartcookie"] != null)
                {
                    var cookie = Request.Cookies["Eshopcartcookie"];
                    var cart = _cartService.GetCartByCookie(cookie);
                    if (cart != null)
                    {
                        var cartdetial = _cartService.GetCartDetailByCartIdAndVariantId(cart.Id, variantid);
                        if (cartdetial != null)
                            update(cartdetial);
                    }
                }
            }


            void update(CartDetail cartDetail)
            {
                var variant = _variantService.GetMinPriceAndCountByVariantId(variantid);
                if (variant.Item2 >= count && variant.Item3 >= count)
                {
                    cartDetail.Count = count;
                }
                else
                {
                    cartDetail.Count = (int)(variant.Item3 >= variant.Item2 ? variant.Item2 : variant.Item3);
                }

                _cartService.UpdateCartDetail(cartDetail);
            }

            return ViewComponent("CartPageComponent");
        }



        public IActionResult RemoveCartDetail(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = int.Parse(User.FindFirst("userid").Value);
                var cart = _cartService.GetCartByUserId(userid);
                if (cart != null)
                {
                    var cartdetial = _cartService.GetCartDetailByCartIdAndVariantId(cart.Id, id);
                    if (cartdetial != null)
                        remove(cartdetial);
                }
            }
            else
            {
                if (Request.Cookies["Eshopcartcookie"] != null)
                {
                    var cookie = Request.Cookies["Eshopcartcookie"];
                    var cart = _cartService.GetCartByCookie(cookie);
                    if (cart != null)
                    {
                        var cartdetial = _cartService.GetCartDetailByCartIdAndVariantId(cart.Id, id);
                        if (cartdetial != null)
                            remove(cartdetial);
                    }
                }
            }


            void remove(CartDetail cartDetail)
            {


                _cartService.RemoveCartDetail(cartDetail);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("Shipping")]
        public IActionResult Shipping()
        {
            TempData["PaymentSetting"] = _siteSettingService.GetSitePaymentSetting();
            var userid = int.Parse(User.FindFirst("userid").Value);

            ViewBag.IsColleague = _userService.IsUserColleague();

            var cartdetail = _cartService.GetCartDetialForShippingPage(userid);
            var cart = _cartService.GetCartByUserId(userid);
            var addresses = _addressService.GetUserAddressesForOrder(userid);
            ViewBag.provincelist = _addressService.GetProvince();
            if (cart.AddressId == null || addresses.All(c => c.AddressId != cart.AddressId))
            {
                if (addresses.Any())
                {
                    if (addresses.FirstOrDefault(c => c.IsDefault) == null)
                    {
                        var res = _addressService.ChangeDefaultUserAddress(Convert.ToInt64(userid), addresses.FirstOrDefault().AddressId);
                        SetCartAddress(addresses.FirstOrDefault().AddressId);
                        cart.AddressId = addresses.FirstOrDefault().AddressId;
                    }
                    else
                    {
                        SetCartAddress(addresses.FirstOrDefault(c => c.IsDefault).AddressId);
                        cart.AddressId = addresses.FirstOrDefault(c => c.IsDefault).AddressId;
                    }


                }
            }
            ViewBag.Address = addresses;

            var shipments = _shipmentService.GetShipmentByAddress(cart.AddressId);
            if (shipments.Any(s => s.ShipmentId == 3))
                shipments.RemoveAll(s => s.Title == "تیپاکس");

            ViewBag.Shipmets = shipments;
            if (cartdetail.Count > 0)
            {
                var user = _userService.GetUserById(userid);
                ShippingPageViewModel model = new ShippingPageViewModel
                {
                    Products = cartdetail,
                };
                if (cart.ShipmentId != null)
                {
                    var cartCount = cartdetail.Sum(c => c.CartCount) - 1;
                    var shipment = _cartService.GetShipmentById(cart.ShipmentId.Value);
                    model.ShippingId = cart.ShipmentId;
                    model.ShippingPrice =
                        shipment.Price + (cartCount * shipment.PricePerAddProduct);
                }

                if (cart.AddressId != null)
                {
                    model.AddressId = cart.AddressId;
                }
                if (user.IsColleague && user.IsCredit && user.AcceptPrice != null && user.AcceptPrice.Value > 0)
                {
                    model.UserCreditPrice = user.AcceptPrice.Value;
                }
                else
                {
                    model.UserCreditPrice = 0;
                }
                return View(model);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [Route("[action]/{Id}")]
        public JsonResult GetShipmentByAddress([FromRoute] int Id)
        {
            var shipments = _shipmentService.GetShipmentByAddress((long)Id);
            if (shipments.Any(s => s.ShipmentId == 3))
                shipments.RemoveAll(s => s.Title == "تیپاکس");

            return Json(JsonConvert.SerializeObject(shipments));
        }
        private bool SetCartAddress(long id)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);

            var cart = _cartService.GetCartByUserId(userid);
            cart.AddressId = id;
            _cartService.UpdateCart(cart);
            return true;
        }
        private bool SetCartShipmet(long id)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);

            var cart = _cartService.GetCartByUserId(userid);
            cart.ShipmentId = id;
            _cartService.UpdateCart(cart);
            return true;
        }

        [Authorize]
        public IActionResult SubmitAddress(long addressId)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            var cart = _cartService.GetCartByUserId(userid);
            var shipments = _shipmentService.GetShipmentByAddress((long)addressId);
            cart.ShipmentId = shipments.First().ShipmentId;
            _cartService.UpdateCart(cart);
            _cartService.UpdateCartAddress(userid, addressId);
            return Json("ok");
        }

        [Authorize]
        public JsonResult SubmitShipping(int id)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            var shipment = _shipmentService.GetShipmentById(id);
            var cartList = new List<CartPageViewModel>();
            if (shipment.CheckPreSell)
            {
                string shipmentAlertMessage = "";
                cartList = _cartService.GetCartDetailForCartPageByUserId(userid);
                foreach (var cart in cartList)
                {
                    var varient = _variantService.GetVariantsId(cart.VariantId);
                    if (varient?.DateSell != null && varient.DateSell > DateTime.Now)
                    {
                        var product = _productService.FindProductById(varient.ProductId);
                        shipmentAlertMessage += "<br> " +
                                                "محصول " + product.FaTitle + " در " + varient.DateSell.Value.ConvertMiladiToShamsi() + " اماده تحویل میباشد.";
                    }
                }

                if (!string.IsNullOrEmpty(shipmentAlertMessage))
                {
                    TempData["ShipmentAlert"] = "کاربر گرامی " + shipmentAlertMessage;
                }
            }

            _cartService.UpdateCartShipment(userid, id);
            return Json(JsonConvert.SerializeObject(new ShipmentViewModel(shipment.Title, shipment.Price)));
        }


        [Authorize]
        [HttpPost]
        public IActionResult CheckDiscountCode(string code)
        {
            var discountcode = _discountCodeService.GetDiscountCode(code);
            if (discountcode != null)
            {
                if (discountcode.StartDate != null && discountcode.StartDate <= DateTime.Now)
                {
                    TempData["DiscountCodeError"] = "تاریخ کد تخفیف درست نیست";
                    return RedirectToAction("Index");
                }
                if (discountcode.EndDate != null && discountcode.EndDate >= DateTime.Now)
                {
                    TempData["DiscountCodeError"] = "تاریخ کد تخفیف درست نیست";
                    return RedirectToAction("Index");
                }

                var userid = int.Parse(User.FindFirst("userid").Value);
                if (discountcode.ForFirstOrder && _orderService.UserExistOrder(userid))
                {
                    TempData["DiscountCodeError"] = "کد تخفیف وارد شده درست نمی باشد";
                    return RedirectToAction("Index");
                }

                if (discountcode.MaxUseCount != null && discountcode.MaxUseCount != 0 && _orderService.GetUserUseDiscountCodeCount(userid, discountcode.Id) >= discountcode.MaxUseCount)
                {
                    TempData["DiscountCodeError"] = "شما قبلا از این کد تخفیف استفاده کرده اید";
                    return RedirectToAction("Index");
                }

                var cartdetial = _cartService.GetCartDetailForCheckDiscountCode(userid);
                int sum = 0;
                if (discountcode.CategoryId != null && discountcode.CategoryId > 0)
                {
                    foreach (var item in cartdetial)
                    {
                        if (item.MainPrice == item.SpecialPrice && item.CategoryId.Any(s => s == discountcode.CategoryId))
                            sum += item.MainPrice * item.Count;
                    }
                }
                else
                {
                    foreach (var item in cartdetial)
                    {
                        if (item.MainPrice == item.SpecialPrice)
                            sum += item.MainPrice * item.Count;
                    }
                }

                if (sum <= 0 || (discountcode.MinOrderAmount != null && discountcode.MinOrderAmount > sum))
                {
                    TempData["DiscountCodeError"] = "کد تخفیف وارد شده درست نمی باشد";
                    return RedirectToAction("Index");
                }

                TempData["DiscountCodeSuccess"] = "کد تخفیف برای شما اعمال شد";
                TempData["DiscountCode"] = code;
                return RedirectToAction("Index");


            }
            else
            {
                TempData["DiscountCodeError"] = "کد تخفیف وارد شده درست نمی باشد";
                return RedirectToAction("Index");
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveDiscountCode(string code)
        {

            TempData["DiscountCodeSuccess"] = "کد تخفیف شما با موفقیت حذف شد";
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        public IActionResult CheckGiftCart(string code)
        {
            var giftcard = _giftCartService.GetGiftCardByCode(code);
            int userid = int.Parse(User.FindFirst("userid").Value);
            if (giftcard != null && giftcard.UserId == null)
            {
                giftcard.UserId = userid;
                if (!_giftCartService.UpdateGiftCard(giftcard))
                    return Json(new { status = false, message = "خطا در ثبت اطلاعات" });
                return Json(new { status = true, amount = giftcard.Balance });
            }
            else
            {
                return Json(new { status = false, message = "کارت هدیه وارد شده اشتباه است" });
            }
        }

        public async Task<IActionResult> SubmitOrder(string discount, string giftcardcode, int typePay, int insurance = -1, int credit = 0/*, [FromServices] IPaymentDetialService _paymentDetialService*/)
        {
            var textInsurance = "";

            var isColleague = _userService.IsUserColleague();
            if (isColleague)
            {
                if (insurance == -1)
                {
                    TempData["ColleagueInsuranceAlert"] = "لطفا درصد بیمه را مشخص کنید";
                    return RedirectToAction(nameof(Shipping));
                }

                textInsurance = insurance == 0 ? "بدون بیمه" : insurance + "درصد بیمه";
            }
            int userid = int.Parse(User.FindFirst("userid").Value);
            var isUserColleague = _userService.IsUserColleague();
            int concurenncycount = 0;
        start:
            var cartdetail = _cartService.GetCartDetialForSubmitOrder(userid);

            if (cartdetail.Count > 0)
            {
                using (var scope = new TransactionScope())
                {


                    #region Changecartdetail

                    foreach (var item in cartdetail)
                    {
                        if (item.RemianingCount > 0)
                        {
                            if (item.RemianingCount < item.CartCount || item.MaxOrderCount < item.CartCount)
                            {
                                scope.Dispose();
                                return RedirectToAction(nameof(Index));
                            }

                            if (item.SpecialPrice != item.CartPrice)
                            {
                                scope.Dispose();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            scope.Dispose();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    #endregion

                    #region ChangeVariantCount And Remove CartDetial
                    List<CartDetail> tempcartdetial = new List<CartDetail>();
                    foreach (var item in cartdetail)
                    {
                        if (item.CartCount > 0)
                        {
                            tempcartdetial.Add(new CartDetail { Id = item.CartDetailId });

                            item.variant.ReserveCount += item.CartCount;
                            //if (item.variant.ShopCount > 0)
                            //    item.variant.ShopCount -= item.CartCount;
                            //else
                            if (isUserColleague)
                                item.variant.Count -= item.CartCount;
                            else
                                item.variant.Count -= item.CartCount;

                            if (!isUserColleague)
                            {
                                if (item.VariantPromotion != null)
                                    item.VariantPromotion.ReminaingCount -= item.CartCount;
                            }

                        }

                    }

                    if (!_variantService.UpdateVariantAndVariantPromotion(cartdetail.Select(c => c.variant).ToList(),
                        cartdetail.Where(c => c.VariantPromotion != null).Select(c => c.VariantPromotion).ToList()))
                    {
                        if (concurenncycount > 1)
                        {
                            scope.Dispose();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            scope.Dispose();
                            concurenncycount += 1;
                            goto start;
                        }
                    }

                    _cartService.RemoveCartDetialList(tempcartdetial);
                    var notactivecartdetail = _cartService.GetNotActiveCartDetail(userid);
                    foreach (var item in notactivecartdetail)
                    {
                        item.IsActiveCart = true;
                    }
                    _cartService.ListUpdateCartDetial(notactivecartdetail);
                    #endregion

                    #region saveorder
                    var user = _userService.GetUserById(userid);
                    if (string.IsNullOrEmpty(user.NatioalCode))
                    {
                        TempData["CompleteProfile"] = true;
                        scope.Dispose();
                        return RedirectToAction(nameof(Shipping));
                    }
                    var cart = _cartService.GetCartByUserId(userid);
                    if (cart.AddressId == null)
                    {
                        TempData["AddAddress"] = true;

                        scope.Dispose();
                        return RedirectToAction(nameof(Shipping));
                    }

                    if (cart.ShipmentId == null)
                    {
                        TempData["SelectShipment"] = true;

                        scope.Dispose();
                        return RedirectToAction(nameof(Shipping));
                    }

                    var shipmet = _shipmentService.GetShipmentById(cart.ShipmentId.Value);
                    if (shipmet == null)
                    {
                        scope.Dispose();
                        return RedirectToAction(nameof(Shipping));
                    }

                    var address = _addressService.GetAddressForOrder(cart.AddressId);
                    var clientAddress = _addressService.GetUserClientAddress(userid);
                    var cartCount = cartdetail.Sum(c => c.CartCount) - 1;
                    var shippingPrice =
                        shipmet.Price + (cartCount * shipmet.PricePerAddProduct);

                    DataLayer.Entities.Order.Order order = new DataLayer.Entities.Order.Order
                    {
                        RecipientAddress = address.ProvinceName
                                           + " , "
                                           + address.CityName
                                           + " , "
                                           + address.PostalAddress
                                           + " -- " + textInsurance

                        ,
                        RecipientName = address.FullName,
                        RecipientPostalCode = address.PostalCode,
                        RecipientTel = address.Phone,
                        ClientName = user.FullName,
                        ClientNatioalCode = user.NatioalCode,
                        ClientTel = user.Phone,
                        ShipmentId = shipmet.Id,
                        ShipmentPrice = shippingPrice * 10,
                        ShipmentTitle = shipmet.TitleCrm,
                        UserId = userid,
                        IsColleauge = isColleague
                    };
                    if (clientAddress != null)
                    {
                        order.ClientAddress = clientAddress.ProvinceName
                                                                  + " , "
                                                                  + clientAddress.CityName
                                                                  + " , "
                                                                  + clientAddress.PostalAddress
                                                                  + " -- " + textInsurance
                                                                  ;
                        order.ClientPostalCode = clientAddress.PostalCode;
                    }
                    else
                    {
                        //Todo Check
                        order.ClientAddress = address.ProvinceName
                                              + " , "
                                              + address.CityName
                                              + " , "
                                              + address.PostalAddress
                                              + " -- " + textInsurance
                            ;
                        order.ClientPostalCode = address.PostalCode;
                    }

                    if (shipmet.NeedGenerateDeliveryCode)
                    {
                        order.NeedUseDeliveryCode = true;
                        order.InPersonCode = CodeGenerator.PasswordGenerator();
                    }
                    var orderid = _orderService.AddOrder(order);

                    #endregion


                    List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
                    #region shipment
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    int sumorderamount = 0;
                    int sumdiscountamount = 0;


                    int tempsum = 0;


                    foreach (var item in cartdetail)
                    {
                        tempsum += item.CartCount * item.SpecialPrice;
                        sumdiscountamount += (item.CartCount * (item.MainPrice - item.SpecialPrice)) * 10;
                        sumorderamount += (item.CartCount * item.SpecialPrice) * 10;
                        orderDetails.Add(new OrderDetail
                        {
                            Count = item.CartCount,
                            Price = item.SpecialPrice * 10,
                            SumPrice = (item.CartCount * item.MainPrice) * 10,
                            Discount = (item.CartCount * (item.MainPrice - item.SpecialPrice)) * 10,
                            SumPriceAfterDiscount = (item.CartCount * item.SpecialPrice) * 10,
                            VariantId = item.VariantId,
                            OrderId = orderid,
                            DiscountType = item.PromotionType,
                            StorePlace = item.Dely <= 0,
                            UnitDiscount = (item.MainPrice - item.SpecialPrice) * 10
                        });
                    }






                    _shipmentService.AddOrderDetailList(orderDetails);
                    #endregion

                    #region CheckDisountCode
                    if (!String.IsNullOrEmpty(discount))
                    {
                        var discountcode = _discountCodeService.GetDiscountCode(discount);
                        if (discountcode != null)
                        {
                            if (discountcode.StartDate != null && discountcode.StartDate <= DateTime.Now)
                                goto checkgiftcard;
                            if (discountcode.EndDate != null && discountcode.EndDate >= DateTime.Now)
                                goto checkgiftcard;


                            if (discountcode.ForFirstOrder && _orderService.UserExistOrder(userid))
                                goto checkgiftcard;

                            if (discountcode.MaxUseCount != null && discountcode.MaxUseCount != 0 && _orderService.GetUserUseDiscountCodeCount(userid, discountcode.Id) >= discountcode.MaxUseCount)
                                goto checkgiftcard;


                            int sum = 0;
                            if (discountcode.CategoryId != null && discountcode.CategoryId > 0)
                            {
                                foreach (var item in cartdetail)
                                {
                                    if (item.MainPrice == item.SpecialPrice && item.CategoryId.Any(s => s == discountcode.CategoryId))
                                        sum += item.MainPrice * item.CartCount;
                                }
                            }
                            else
                            {
                                foreach (var item in cartdetail)
                                {
                                    if (item.MainPrice == item.SpecialPrice)
                                        sum += item.MainPrice * item.CartCount;
                                }
                            }

                            if (sum <= 0 || (discountcode.MinOrderAmount != null && discountcode.MinOrderAmount > sum))
                                goto checkgiftcard;
                            else
                            {
                                paymentDetails.Add(new PaymentDetail
                                {
                                    Date = DateTime.Now,
                                    DiscountCodeId = discountcode.Id,
                                    OrderId = orderid,
                                    Price = discountcode.Price,
                                    Type = EnumPaymentType.Discount,
                                    State = true,

                                });
                                sumorderamount -= discountcode.Price;
                            }
                        }
                    }
                    #endregion

                    sumorderamount += shippingPrice * 10;

                //    #region CheckCredit
                //    if (credit > 0)
                //    {
                //        if (user.IsColleague && user.IsCredit && user.AcceptPrice != null && user.AcceptPrice >= credit)
                //        {
                //            var creditPriceRial = credit * 10;
                //            var temp = new PaymentDetail
                //            {
                //                Date = DateTime.Now,
                //                Type = EnumPaymentType.Credit,
                //                OrderId = orderid,
                //                State = true,
                //            };



                //            if (creditPriceRial >= sumorderamount)
                //            {
                //                temp.Price = sumorderamount;
                //                user.AcceptPrice -= credit;
                //                sumorderamount = 0;
                //            }
                //            else
                //            {
                //                temp.Price = creditPriceRial;
                //                sumorderamount -= creditPriceRial;
                //                user.AcceptPrice -= credit;


                //            }
                //            if (user.AcceptPrice < 0)
                //                user.AcceptPrice = 0;
                //            paymentDetails.Add(temp);
                //            _userService.EditUser(user);
                //        }


                //    }
                //#endregion
                #region CheckGiftCard
                checkgiftcard:
                    if (!String.IsNullOrEmpty(giftcardcode))
                    {
                        var giftcard = _giftCartService.GetGiftCardByCode(giftcardcode);
                        if (giftcard != null && giftcard.UserId == userid)
                        {
                            var temp = new PaymentDetail
                            {
                                Date = DateTime.Now,
                                Type = EnumPaymentType.Discount,
                                OrderId = orderid,
                                GiftCartId = giftcard.Id,
                                State = true,
                            };

                            var giftcarttransaction = new GiftCardTransaction
                            {
                                OrderId = orderid,
                                GiftCardId = giftcard.Id,
                            };

                            if (giftcard.Balance >= sumorderamount)
                            {
                                temp.Price = sumorderamount;
                                giftcard.Balance -= sumorderamount;
                                sumorderamount = 0;
                                giftcarttransaction.Price = sumorderamount;
                            }
                            else
                            {
                                temp.Price = giftcard.Balance;
                                sumorderamount -= giftcard.Balance;
                                giftcarttransaction.Price = giftcard.Balance;
                                giftcard.Balance = 0;


                            }

                            paymentDetails.Add(temp);
                            _giftCartService.UpdateGiftCard(giftcard);
                            _giftCartService.AddGiftCardTransaction(giftcarttransaction);
                        }
                    }
                    #endregion
                    #region Payment
                    order.Id = orderid;
                    if (sumorderamount > 0)
                    {
                        paymentDetails.Add(new PaymentDetail
                        {
                            Date = DateTime.Now,
                            OrderId = orderid,
                            Price = sumorderamount,
                            State = false,
                            Type = EnumPaymentType.Online,
                            PaymentStatus = EnumPaymentStatus.Wait,
                            TypeBank = typePay
                        });
                        _paymentDetialService.AddRangePaymentDetial(paymentDetails);
                        order.AmountPayable = sumorderamount;
                        order.OrderStatus = EnumOrderStatus.NotPay;
                        order.SumAmount = sumorderamount + sumdiscountamount;
                        _orderService.UpdateOrder(order);

                        if (typePay == 1)
                        {
                            try
                            {
                                long orderID = (long)orderid;
                                long priceAmount = (long)sumorderamount;

                                long payerId = 0;
                                string additionalText = "توضیحات";
                                BankMellatImplement bankMellatImplement = new BankMellatImplement();
                                string resultRequest = bankMellatImplement.bpPayRequest(orderID, priceAmount, additionalText, payerId);
                                string[] StatusSendRequest = resultRequest.Split(',');
                                if (int.Parse(StatusSendRequest[0]) == 0/*(int)BankMellatImplement.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ*/)
                                {
                                    if (StatusSendRequest[1] == null)
                                    {

                                        TempData["Message"] = "";

                                        return RedirectToAction("CheckOut");
                                    }
                                    else
                                    {
                                        var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderID);
                                        paydet.RefId = StatusSendRequest[1];
                                        _paymentDetialService.UpdatePaymentDetail(paydet);
                                        ViewBag.Mablagh = priceAmount;
                                        ViewBag.id = StatusSendRequest[1];
                                        ViewBag.TypePay = typePay;

                                        scope.Complete();

                                        using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                        {
                                            var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                        }

                                        return View();
                                    }

                                }

                                TempData["Message"] = bankMellatImplement.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " ")));

                                return RedirectToAction("CheckOut");
                            }
                            catch (Exception Error)
                            {
                                TempData["Message"] =
                                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                    + Environment.NewLine
                                    + Error.Message
                                    ;//Exceptions.TarakoneshMojadad;
                                ;
                                return RedirectToAction("CheckOut");
                            }


                        }
                        else if (typePay == 2)
                        {
                            try
                            {

                                var localDate = DateTime.Now.ToString("yyyyMMdd HHmmss");
                                ApiCallerV2 apiCaller = new ApiCallerV2(AsanPardakhtHelper.GetTokenUrl());
                                TokenRequestData tokenRequestData = new TokenRequestData
                                {
                                    additionalData = "فروشگاه اینترنتی فرنا",
                                    amountInRials = sumorderamount,
                                    callbackURL = AsanPardakhtHelper.GetCallBackUrl() + "?id=" + orderid,
                                    localDate = localDate,
                                    localInvoiceId = orderid,
                                    merchantConfigurationId = AsanPardakhtHelper.GetMerchantConfigurationId(),
                                    paymentId = 0,
                                    serviceTypeId = AsanPardakhtHelper.GetServiceTypeId(),
                                    settlementPortions = new List<SettlementPortion>
                                    {
                                        new SettlementPortion
                                        {
                                            amountInRials = sumorderamount,
                                            paymentId = 0,
                                            iban = AsanPardakhtHelper.GetIban()
                                        }
                                    }
                                };
                                KeyValuePair<string, object>[] headers =   {
                                    new KeyValuePair<string, object>("usr",AsanPardakhtHelper.GetUsername()),
                                    new KeyValuePair<string, object>("pwd",AsanPardakhtHelper.GetPassword()),
                                 };
                                var data = apiCaller.Call(HttpMethod.Post, headers, null, tokenRequestData);
                                AsanPardakhtLog asanPardakhtLog = new AsanPardakhtLog
                                {
                                    Contents = JsonConvert.SerializeObject(tokenRequestData),
                                    Headers = JsonConvert.SerializeObject(headers),
                                    OrderId = orderid,
                                    Params = "",
                                    PaymentDetailId = paymentDetails.FirstOrDefault()?.Id,
                                    Result = JsonConvert.SerializeObject(data.Data),
                                    Url = AsanPardakhtHelper.GetTokenUrl(),
                                    Status = data.IsSuccess,
                                    UserId = userid,
                                    Method = "Token",
                                    Type = "Post"
                                };
                                _orderService.AddAsanPardakhtLog(asanPardakhtLog);
                                if (data.IsSuccess)
                                {
                                    var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderid);
                                    paydet.RefId = data.Data.ToString();
                                    _paymentDetialService.UpdatePaymentDetail(paydet);
                                    ViewBag.Mablagh = sumorderamount;
                                    ViewBag.id = data.Data.ToString();
                                    ViewBag.TypePay = typePay;
                                    scope.Complete();

                                    using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                    {
                                        var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                    }

                                    return View();
                                }
                                else
                                {
                                    var res = JsonConvert.SerializeObject(data.Data.ToString());
                                    TempData["Message"] = res;

                                    return RedirectToAction("CheckOut");
                                }

                            }
                            catch (Exception Error)
                            {
                                TempData["Message"] =
                                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                    + Environment.NewLine
                                    + Error.Message
                                    ;//Exceptions.TarakoneshMojadad;
                                ;
                                return RedirectToAction("CheckOut");
                            }
                        }
                        else if (typePay == 3)
                        {
                            try
                            {
                                //New
                                RequestParameters parameters = new RequestParameters(ZarinPalHelper.GetMerchant(), sumorderamount.ToString(), "فروشگاه اینترنتی فرنا", ZarinPalHelper.GetCallBackUrl(), "", "");



                                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                                var client = new RestSharp.RestClient(ZarinPalHelper.GetRequestUrl());

                                Method method = Method.Post;

                                var request = new RestRequest("", method);

                                request.AddHeader("accept", "application/json");

                                request.AddHeader("content-type", "application/json");

                                request.AddJsonBody(parameters);

                                var requestresponse = client.ExecuteAsync(request);

                                JObject jo = JObject.Parse(requestresponse.Result.Content);

                                string errorscode = jo["errors"].ToString();

                                JObject jodata = JObject.Parse(requestresponse.Result.Content);

                                string dataauth = jodata["data"].ToString();


                                if (dataauth != "[]")
                                {


                                    ZarinpalAuthority = jodata["data"]["authority"].ToString();

                                    string gatewayUrl = ZarinPalHelper.GetGateWayUrl() + ZarinpalAuthority;

                                    var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderid);
                                    paydet.Authority = ZarinpalAuthority;
                                    _paymentDetialService.UpdatePaymentDetail(paydet);

                                    scope.Complete();

                                    using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                    {
                                        var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                    }
                                    //
                                    return Redirect(gatewayUrl);
                                }
                                else
                                {


                                    var res = JsonConvert.SerializeObject(errorscode);
                                    TempData["Message"] = res;

                                    return RedirectToAction("CheckOut");

                                }

                                //
                                //JObject jo = null;
                                //string errorscode = null;

                                //JObject jodata = null;
                                //string dataauth = null;
                                //using (var client = new HttpClient())
                                //{
                                //    RequestParameters parameters = new RequestParameters(ZarinPalHelper.GetMerchant(), sumorderamount.ToString(), "فروشگاه اینترنتی فرنا", ZarinPalHelper.GetCallBackUrl(), "", "");

                                //    var json = JsonConvert.SerializeObject(parameters);

                                //    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                                //    HttpResponseMessage response = await client.PostAsync(ZarinPalHelper.GetRequestUrl(), content);

                                //    string responseBody = await response.Content.ReadAsStringAsync();

                                //     jo = JObject.Parse(responseBody);
                                //     errorscode = jo["errors"].ToString();

                                //     jodata = JObject.Parse(responseBody);
                                //     dataauth = jodata["data"].ToString();




                                //}
                                //if (dataauth != "[]")
                                //{


                                //    ZarinpalAuthority = jodata["data"]["authority"].ToString();

                                //    //
                                //    var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderid);
                                //    paydet.Authority = ZarinpalAuthority;
                                //    _paymentDetialService.UpdatePaymentDetail(paydet);

                                //    string gatewayUrl = ZarinPalHelper.GetGateWayUrl() + ZarinpalAuthority;

                                //    // ViewBag.Mablagh = sumorderamount;
                                //    // ViewBag.id = data.Data.ToString();
                                //    // ViewBag.TypePay = typePay;
                                //    scope.Complete();

                                //    using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                //    {
                                //        var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                //    }
                                //    //
                                //    return Redirect(gatewayUrl);

                                //}
                                //else
                                //{

                                //    var res = JsonConvert.SerializeObject(errorscode);
                                //    TempData["Message"] = res;

                                //    return RedirectToAction("CheckOut");

                                //}

                            }
                            catch (Exception Error)
                            {
                                TempData["Message"] =
                                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                    + Environment.NewLine
                                    + Error.Message
                                    ;//Exceptions.TarakoneshMojadad;
                                ;
                                return RedirectToAction("CheckOut");
                            }
                        }
                        else if (typePay == 4)
                        {
                            try
                            {
                                WebHelper webHelper = new WebHelper();



                                string request = string.Empty;
                                IPGData iPGData = new IPGData();
                                iPGData.TreminalId = IranKishHelper.GetTreminalId();
                                iPGData.AcceptorId = IranKishHelper.GetAcceptorId();

                                iPGData.RevertURL = IranKishHelper.GetCallBackUrl();
                                iPGData.Amount = sumorderamount;
                                iPGData.PaymentId = order.Id.ToString();
                                iPGData.RequestId = order.Id.ToString();
                                iPGData.CmsPreservationId = IranKishHelper.GetCmsPreservationId();
                                iPGData.TransactionType = TransactionType.Purchase;
                                iPGData.BillInfo = null;
                                iPGData.PassPhrase = IranKishHelper.GetPassPhrase();
                                iPGData.RsaPublicKey = IranKishHelper.GetRSAPublicKey();

                                request = CreateJsonRequest.CreateJasonRequest(iPGData);

                                Uri url = new Uri(IranKishHelper.GetTokenUrl());
                                string jresponse = webHelper.Post(url, request);

                                if (jresponse != null)
                                {
                                    TokenResult jResult = JsonConvert.DeserializeObject<TokenResult>(jresponse);
                                    //Handle your reponse here 

                                    if (jResult.status)
                                    {
                                        var token = jResult.result?.token;

                                        var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderid);
                                        paydet.Authority = token;
                                        _paymentDetialService.UpdatePaymentDetail(paydet);
                                        //
                                        scope.Complete();

                                        using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                        {
                                            var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                        }
                                        //
                                        TempData["PaymentUrl"] = IranKishHelper.GetPaymentUrl();
                                        TempData["Token"] = token;

                                        return View("SubmitOrderIranKish");


                                    }
                                    else
                                    {
                                        var msg = string.Format("result:{0} desc:{1}", jResult.responseCode, jResult.description);
                                        TempData["Message"] =
                         "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                         + Environment.NewLine
                         + msg
                         ;//Exceptions.TarakoneshMojadad;
                                        ;
                                        return RedirectToAction("CheckOut");
                                    }
                                }




                            }
                            catch (Exception exe)
                            {

                                TempData["Message"] =
                                "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                + Environment.NewLine
                                + exe.Message
                                ;//Exceptions.TarakoneshMojadad;
                                ;
                                return RedirectToAction("CheckOut");
                            }
                        }
                        else if (typePay == 5)
                        {
                            try
                            {
                                long orderID = (long)orderid;
                                long priceAmount = (long)sumorderamount;

                                PaymentRequest request = new PaymentRequest
                                {
                                    OrderId = orderID.ToString(),
                                    MerchantId = MelliHelper.GetMerchantId(),
                                    TerminalId = MelliHelper.GetTerminalId(),
                                    MerchantKey = MelliHelper.GetMerchantKey(),
                                    Amount = priceAmount,
                                    PurchasePage = MelliHelper.GetPurchasePage()
                                };

                                var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", request.TerminalId, request.OrderId, request.Amount));

                                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                                symmetric.Mode = CipherMode.ECB;
                                symmetric.Padding = PaddingMode.PKCS7;

                                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);

                                request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                                request.ReturnUrl = MelliHelper.GetCallBackUrl();

                                var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", request.PurchasePage);


                                //HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", JsonConvert.SerializeObject(request));
                                //Response.Cookies.Add(merchantTerminalKeyCookie);

                                var data = new
                                {
                                    request.TerminalId,
                                    request.MerchantId,
                                    request.Amount,
                                    request.SignData,
                                    request.ReturnUrl,
                                    LocalDateTime = DateTime.Now,
                                    request.OrderId,
                                    //MultiplexingData = request.MultiplexingData
                                };

                                var res = CallApi<PayResultData>(ipgUri, data);
                                res.Wait();

                                if (res != null && res.Result != null)
                                {
                                    if (res.Result.ResCode == "0")
                                    {
                                        //
                                        var paydet = _paymentDetialService.GetOnliyTypePaymentDetail(orderID);
                                        paydet.Authority = res.Result.Token;
                                        _paymentDetialService.UpdatePaymentDetail(paydet);
                                        ViewBag.Mablagh = priceAmount;
                                        ViewBag.id = res.Result.Token;
                                        ViewBag.TypePay = typePay;

                                        scope.Complete();

                                        using (TransactionScope tsSuppressed = new TransactionScope(TransactionScopeOption.Suppress))
                                        {
                                            var jobId = BackgroundJob.Schedule(() => _orderService.CheckOrderPaymentVerify(order.Id), TimeSpan.FromMinutes(20));
                                        }

                                        //

                                       return Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
                                    }
                                    ViewBag.Message = res.Result.Description;
                                    TempData["Message"] =
                                        "-S1-"+
                                        "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                        + Environment.NewLine
                                        + res.Result.Description
                                        ;//Exceptions.TarakoneshMojadad;
                                    ;
                                    return RedirectToAction("CheckOut");
                                }
                                

                            }
                            catch (Exception Error)
                            {
                                TempData["Message"] = "-S2-" +
                                                      "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                                      + Environment.NewLine
                                                      + Error.Message
                                    ;//Exceptions.TarakoneshMojadad;
                                ;
                                return RedirectToAction("CheckOut");
                            }


                        }

                        TempData["Message"] = "-S3-" +
                                              "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";
                        return RedirectToAction("CheckOut");
                    }
                    else
                    {
                        _paymentDetialService.AddRangePaymentDetial(paymentDetails);
                        order.AmountPayable = 0;
                        order.OrderStatus = EnumOrderStatus.InProccess;
                        order.SumAmount = 0;
                        order.IsCheckPayState = true;
                        _orderService.UpdateOrder(order);

                        scope.Complete();
                        SendOrderToCrm(orderid);

                        return RedirectToAction("orders", "Profile");
                    }
                    #endregion
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
        public ActionResult CheckOut(CheckOutPaymentVM payment)
        {
            return View(payment);
        }

        [HttpPost]
        public ActionResult VerfiyOrder(string RefId, string ResCode, long saleOrderId, long saleReferenceId, string CardHolderPAN)
        {
            bool runBpReversalRequest = false;
            string resultCodeBpPayRequest;

            BankMellatImplement bankMellatImplement = new BankMellatImplement();

            try
            {
                var saleReference = saleReferenceId;
                resultCodeBpPayRequest = ResCode;
                string resultCodeBpinquiryRequest = "-9999";
                string resultCodeBpSettleRequest = "-9999";
                string resultCodeBpVerifyRequest = "-9999";
                EnumVaziyatPardakht vaziyatPardakht = EnumVaziyatPardakht.PardakhtNashode;
                if (int.Parse(resultCodeBpPayRequest) == 0)
                {
                    #region Success
                    saleReferenceId = Convert.ToInt64(saleReference);
                    resultCodeBpVerifyRequest = bankMellatImplement.VerifyRequest(saleOrderId, saleOrderId, saleReferenceId);

                    if (string.IsNullOrEmpty(resultCodeBpVerifyRequest))
                    {
                        #region Inquiry Request

                        resultCodeBpinquiryRequest = bankMellatImplement.InquiryRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if (int.Parse(resultCodeBpinquiryRequest) != 0)
                        {
                            var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpinquiryRequest.Replace("_", " ")));
                            TempData["Message"] = message;
                            runBpReversalRequest = true;
                            UpdatePaymentRequest(saleOrderId, saleReferenceId, resultCodeBpVerifyRequest, message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Error, false);

                        }

                        #endregion
                    }

                    if ((int.Parse(resultCodeBpVerifyRequest) == 0)
                        ||
                        (int.Parse(resultCodeBpinquiryRequest) == 0))
                    {

                        #region SettleRequest

                        resultCodeBpSettleRequest = bankMellatImplement.SettleRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if ((int.Parse(resultCodeBpSettleRequest) == 0)
                            || (int.Parse(resultCodeBpSettleRequest) == 45/*(int)BankMellatImplement.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ)*/))
                        {
                            vaziyatPardakht = EnumVaziyatPardakht.PardakhtShode;
                            TempData["id"] = 0;//Manager.GetBimeNameByOrderId(saleOrderId).ID;
                            var message = "تراکنش شما با موفقیت انجام شد";//Exceptions.TarakoneshMovafagh;
                            message += "لطفا شماره پیگیری را یادداشت نمایید" + saleReferenceId;
                            TempData["Message"] = message;
                            UpdatePaymentRequest(saleOrderId, saleReferenceId, resultCodeBpSettleRequest, message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Pay, true);

                        }
                        else
                        {
                            var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpSettleRequest.Replace("_", " ")));
                            TempData["Message"] = message;
                            runBpReversalRequest = true;
                            UpdatePaymentRequest(saleOrderId, saleReferenceId, resultCodeBpSettleRequest, message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Error, false);

                        }

                        #endregion
                    }
                    else
                    {
                        var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpVerifyRequest.Replace("_", " ")));
                        TempData["Message"] = message;
                        runBpReversalRequest = true;
                        UpdatePaymentRequest(saleOrderId, saleReferenceId, resultCodeBpVerifyRequest, message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Error, false);

                    }

                    #endregion
                }
                else
                {
                    var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpPayRequest)).Replace("_", " ");
                    UpdatePaymentRequest(saleOrderId, saleReferenceId, resultCodeBpPayRequest, message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Error, false);

                    TempData["Message"] = message;
                    runBpReversalRequest = true;
                }

                var payment = _paymentDetialService.GetCheckOutPayment(saleOrderId);
                return RedirectToAction("CheckOut", payment);
            }
            catch (Exception error)
            {
                var message =
                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                    + Environment.NewLine
                    + error.Message;//Exceptions.TarakoneshMojadad;
                TempData["Message"] = message;
                runBpReversalRequest = true;
                UpdatePaymentRequest(saleOrderId, saleReferenceId, "-1", message, DateTime.Now, CardHolderPAN, EnumPaymentStatus.Error, false);

                var payment = _paymentDetialService.GetCheckOutPayment(saleOrderId);

                return RedirectToAction("CheckOut", payment);
            }
            finally
            {
                if (runBpReversalRequest)
                {
                    if (saleOrderId != -999 && saleReferenceId != -999)
                        bankMellatImplement.bpReversalRequest(saleOrderId, saleOrderId, saleReferenceId);

                }
            }

        }
        public ActionResult VerfiyOrderAp(string id)
        {

            int userid = 0;//int.Parse(User.FindFirst("userid").Value); 
            long orderId = 0;
            try
            {
                orderId = Convert.ToInt64(id);
                var paymentDetail = _paymentDetialService.GetOnliyTypePaymentDetail(orderId);
                if (paymentDetail.PaymentStatus != EnumPaymentStatus.Pay)
                {
                    ApiCallerV2 apiCaller = new ApiCallerV2(AsanPardakhtHelper.GetTranResultUrl() + "?LocalInvoiceId=" + orderId + "&MerchantConfigurationId=" + AsanPardakhtHelper.GetMerchantConfigurationId());

                    KeyValuePair<string, object>[] parameters =   {
                                    new KeyValuePair<string, object>("LocalInvoiceId", orderId),
                                    new KeyValuePair<string, object>("MerchantConfigurationId", AsanPardakhtHelper.GetMerchantConfigurationId()),
                                 };
                    KeyValuePair<string, object>[] headers =   {
                                    new KeyValuePair<string, object>("usr",AsanPardakhtHelper.GetUsername()),
                                    new KeyValuePair<string, object>("pwd",AsanPardakhtHelper.GetPassword()),
                                 };
                    var data = apiCaller.Call(HttpMethod.Get, headers, null, null);
                    AsanPardakhtLog asanPardakhtLog = new AsanPardakhtLog
                    {
                        Contents = null,
                        Headers = JsonConvert.SerializeObject(headers),
                        OrderId = orderId,
                        Params = JsonConvert.SerializeObject(parameters),
                        PaymentDetailId = paymentDetail.Id,
                        Result = JsonConvert.SerializeObject(data.Data),
                        Url = AsanPardakhtHelper.GetTranResultUrl(),
                        Status = data.IsSuccess,
                        UserId = userid,
                        Method = "TranResult",
                        Type = "Get"
                    };
                    _orderService.AddAsanPardakhtLog(asanPardakhtLog);
                    if (data.IsSuccess && data.Data != null)
                    {
                        var tranResult = JsonConvert.DeserializeObject<TranResultResponse>(data.Data.ToString());
                        if (tranResult.amount != paymentDetail.Price.ToString() ||
                            tranResult.refID != paymentDetail.RefId)
                        {
                            var res = JsonConvert.SerializeObject(data.Data.ToString());
                            UpdatePaymentRequest(orderId, 0, "", res, DateTime.Now, "", EnumPaymentStatus.Error, false);
                            var message =
                               "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                               ;

                            TempData["Message"] = message;
                        }
                        else
                        {
                            ApiCallerV2 apiCallerVerify = new ApiCallerV2(AsanPardakhtHelper.GetVerifyUrl());

                            VerifyRequest verifyRequest = new VerifyRequest
                            {
                                merchantConfigurationId = AsanPardakhtHelper.GetMerchantConfigurationId(),
                                payGateTranId = Convert.ToInt64(tranResult.payGateTranID)
                            };
                            KeyValuePair<string, object>[] headersVerify =   {
                                new KeyValuePair<string, object>("usr",AsanPardakhtHelper.GetUsername()),
                                new KeyValuePair<string, object>("pwd",AsanPardakhtHelper.GetPassword()),
                            };
                            var dataVerify = apiCallerVerify.Call(HttpMethod.Post, headersVerify, null, verifyRequest);
                            AsanPardakhtLog asanPardakhtLogVerify = new AsanPardakhtLog
                            {
                                Contents = JsonConvert.SerializeObject(verifyRequest),
                                Headers = JsonConvert.SerializeObject(headersVerify),
                                OrderId = orderId,
                                Params = "",//JsonConvert.SerializeObject(parameters),
                                PaymentDetailId = paymentDetail.Id,
                                Result = JsonConvert.SerializeObject(dataVerify.Data),
                                Url = AsanPardakhtHelper.GetVerifyUrl(),
                                Status = dataVerify.IsSuccess,
                                UserId = userid,
                                Method = "Verify",
                                Type = "Post"
                            };
                            _orderService.AddAsanPardakhtLog(asanPardakhtLogVerify);

                            if (dataVerify.IsSuccess)
                            {
                                UpdatePaymentRequest(orderId, Convert.ToInt64(tranResult.rrn), tranResult.payGateTranID, tranResult.payGateTranDateEpoch.ToString(), DateTime.Now, tranResult.cardNumber, EnumPaymentStatus.Pay, true);
                                ApiCallerV2 apiCallerSettlement = new ApiCallerV2(AsanPardakhtHelper.GetSettlementUrl());

                                VerifyRequest SettlementRequest = new VerifyRequest
                                {
                                    merchantConfigurationId = AsanPardakhtHelper.GetMerchantConfigurationId(),
                                    payGateTranId = Convert.ToInt64(tranResult.payGateTranID)
                                };
                                KeyValuePair<string, object>[] headersSettlement =   {
                                    new KeyValuePair<string, object>("usr",AsanPardakhtHelper.GetUsername()),
                                    new KeyValuePair<string, object>("pwd",AsanPardakhtHelper.GetPassword()),
                                };
                                var dataSettlement = apiCallerSettlement.Call(HttpMethod.Post, headersSettlement, null, SettlementRequest);
                                AsanPardakhtLog asanPardakhtLogSettlement = new AsanPardakhtLog
                                {
                                    Contents = JsonConvert.SerializeObject(SettlementRequest),
                                    Headers = JsonConvert.SerializeObject(headersSettlement),
                                    OrderId = orderId,
                                    Params = "",//JsonConvert.SerializeObject(parameters),
                                    PaymentDetailId = paymentDetail.Id,
                                    Result = JsonConvert.SerializeObject(dataSettlement.Data),
                                    Url = AsanPardakhtHelper.GetSettlementUrl(),
                                    Status = dataSettlement.IsSuccess,
                                    UserId = userid,
                                    Method = "Settlement",
                                    Type = "Post"
                                };
                                _orderService.AddAsanPardakhtLog(asanPardakhtLogSettlement);
                            }
                            else
                            {
                                var res = JsonConvert.SerializeObject(dataVerify.Data.ToString());
                                TempData["Message"] = res;
                                UpdatePaymentRequest(orderId, 0, "", res, DateTime.Now, "", EnumPaymentStatus.Error, false);

                                return RedirectToAction("CheckOut");
                            }
                        }


                    }
                    else
                    {
                        if (data.Data != null)
                        {
                            var res = JsonConvert.SerializeObject(data.Data.ToString());
                            TempData["Message"] = res;

                        }
                        UpdatePaymentRequest(orderId, 0, "", "کاربر منصرف شده 001", DateTime.Now, "", EnumPaymentStatus.Error, false);


                        return RedirectToAction("CheckOut");
                    }
                }



            }
            catch (Exception error)
            {
                TempData["Message"] = error.Message;
                UpdatePaymentRequest(orderId, 0, "", error.Message, DateTime.Now, "", EnumPaymentStatus.Error, false);


            }
            var payment = _paymentDetialService.GetCheckOutPayment(orderId);
            return RedirectToAction("CheckOut", payment);

        }
        public async Task<IActionResult> VerfiyOrderZarinPal()
        {
            try
            {

                VerifyParameters parameters = new VerifyParameters();


                if (HttpContext.Request.Query["Authority"] != "")
                {
                    ZarinpalAuthority = HttpContext.Request.Query["Authority"];
                }

                var paymentDetail = _paymentDetialService.GetPaymentDetailByAuthority(ZarinpalAuthority);
                if (paymentDetail == null)
                {
                    var message =
                        "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";
                    TempData["Message"] = message;
                    return RedirectToAction("CheckOut", null);
                }
                parameters.authority = ZarinpalAuthority;

                parameters.amount = paymentDetail.Price.ToString();

                parameters.merchant_id = ZarinPalHelper.GetMerchant();


                using (HttpClient client = new HttpClient())
                {

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ZarinPalHelper.GetVerifyUrl(), content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);

                    string errors = jo["errors"].ToString();

                    if (data != "[]")
                    {
                        string refid = jodata["data"]["ref_id"].ToString();

                        TempData["id"] = 0;//Manager.GetBimeNameByOrderId(saleOrderId).ID;
                        var message = "تراکنش شما با موفقیت انجام شد";//Exceptions.TarakoneshMovafagh;
                        message += "لطفا شماره پیگیری را یادداشت نمایید" + refid;
                        TempData["Message"] = message;
                        UpdatePaymentRequest(paymentDetail.OrderId, Convert.ToInt64(refid), "1", message, DateTime.Now, "", EnumPaymentStatus.Pay, true);

                        var payment = _paymentDetialService.GetCheckOutPayment(paymentDetail.OrderId);
                        return RedirectToAction("CheckOut", payment);
                    }
                    else if (errors != "[]")
                    {

                        string errorscode = jo["errors"]["code"].ToString();

                        var message =
                            "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                            + Environment.NewLine
                            + errorscode;//Exceptions.TarakoneshMojadad;
                        TempData["Message"] = message;
                        UpdatePaymentRequest(paymentDetail.OrderId, 0, "-1", message, DateTime.Now, "", EnumPaymentStatus.Error, false);

                        var payment = _paymentDetialService.GetCheckOutPayment(paymentDetail.OrderId);

                        return RedirectToAction("CheckOut", payment);
                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult VerfiyOrderIranKish()
        {
            long orderId = 0;
            string extMessage = "";
            try
            {
                //lblParam.InnerText = string.Format("Acceptor={0} _ Amount={1} _ PaymentId={2} _ InvoiceNumber={3} , RequestString:{4} ",
                //    Request["acceptorId"], Request["amount"], Request["paymentId"], Request["requestId"], Request.Form.ToString());
                //txtAcceptorId.Text = Request["acceptorId"].Substring(7, 8);
                //lblAmount.InnerText = Request["amount"];
                //txtRRN.Text = Request["retrievalReferenceNumber"];
                //txtTraceNo.Text = Request["systemTraceAuditNumber"];
                extMessage +=    string.Format("Acceptor={0} _ Amount={1} _ PaymentId={2} _ InvoiceNumber={3} , RequestString:{4} ",
                    HttpContext.Request.Form["acceptorId"], HttpContext.Request.Form["amount"], HttpContext.Request.Form["paymentId"], HttpContext.Request.Form["requestId"], HttpContext.Request.Form.ToString());
                var responseCode = HttpContext.Request.Form["responseCode"];
                var token = HttpContext.Request.Form["token"];
                var acceptorId = HttpContext.Request.Form["acceptorId"];
                var amount = HttpContext.Request.Form["amount"];
                var paymentId = HttpContext.Request.Form["paymentId"];
                var requestId = HttpContext.Request.Form["requestId"];
                var rnn = HttpContext.Request.Form["retrievalReferenceNumber"];
                var traceNo = HttpContext.Request.Form["systemTraceAuditNumber"];

                //
                orderId = Convert.ToInt64(requestId);
                var paymentDetail = _paymentDetialService.GetOnliyTypePaymentDetail(orderId);
                if (paymentDetail?.PaymentStatus != EnumPaymentStatus.Pay)
                {
                    if (!string.IsNullOrEmpty(responseCode) && responseCode == "00")
                    {
                        WebHelper webHelper = new WebHelper();


                        RequestVerify requestVerify = new RequestVerify();
                        requestVerify.terminalId = IranKishHelper.GetTreminalId();
                        requestVerify.systemTraceAuditNumber = traceNo;
                        requestVerify.retrievalReferenceNumber = rnn;
                        requestVerify.tokenIdentity = token;


                        string request = JsonConvert.SerializeObject(requestVerify);


                        var urlVerify = IranKishHelper.GetVerifyUrl();

                        Uri url = new Uri(urlVerify);
                        string jresponse = webHelper.Post(url, request);


                        if (jresponse != null)
                        {
                            VerifyResult jResult = JsonConvert.DeserializeObject<VerifyResult>(jresponse);
                            //Handle your reponse here


                            if (jResult.status)
                            {

                                string refid = rnn;

                                TempData["id"] = 0;//Manager.GetBimeNameByOrderId(saleOrderId).ID;
                                var message = "تراکنش شما با موفقیت انجام شد";//Exceptions.TarakoneshMovafagh;
                                message += "لطفا شماره پیگیری را یادداشت نمایید" + refid;
                                message += jResult.description;
                                TempData["Message"] = message;
                                UpdatePaymentRequest(paymentDetail.OrderId, Convert.ToInt64(rnn), "1", message, DateTime.Now, "", EnumPaymentStatus.Pay, true);

                                var payment = _paymentDetialService.GetCheckOutPayment(paymentDetail.OrderId);
                                return RedirectToAction("CheckOut", payment);
                                //  Verification succed , your statements Goes here

                            }
                            else
                            {
                                var res = jResult.description;
                                UpdatePaymentRequest(orderId, 0, "", res, DateTime.Now, "", EnumPaymentStatus.Error, false);
                                var message =
                                   "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                                   + Environment.NewLine +
                                   res;

                                TempData["Message"] = message + " ---- " + extMessage;
                            }
                        }
                    }
                    else
                    {
                        UpdatePaymentRequest(orderId, 0, "", responseCode, DateTime.Now, "", EnumPaymentStatus.Error, false);
                        var message =
                           "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                           ;

                        TempData["Message"] = message + " ---- " + extMessage;
                    }

                }



            }
            catch (Exception error)
            {
                TempData["Message"] = error.Message + " - "+ error.InnerException.Message + " - " + error  +" ---- "+ extMessage;
                if (orderId != 0)
                {
                    UpdatePaymentRequest(orderId, 0, "", error.Message, DateTime.Now, "", EnumPaymentStatus.Error, false);

                }


            }
            var payment2 = _paymentDetialService.GetCheckOutPayment(orderId);
            return RedirectToAction("CheckOut", payment2);

        }
        [HttpPost]
        public ActionResult VerfiyOrderMelli(PurchaseResult result)
        {
            long orderId = 0;
            try
            {
                // var cookie = Request.Cookies["Data"].Value;
                //var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);
                orderId = Convert.ToInt64(result.OrderId);
                var paymentDetail = _paymentDetialService.GetOnliyTypePaymentDetail(orderId);
                if (paymentDetail.PaymentStatus != EnumPaymentStatus.Pay)
                {
                    if (result.Token != paymentDetail.Authority)
                    {
                         UpdatePaymentRequest(orderId, 0, "", "Token Not Valid", DateTime.Now, "", EnumPaymentStatus.Error, false);
                        var message =
                                "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                            + "  -  Token Not Valid"
                            ;

                        TempData["Message"] = message;
                    }
                    else
                    {
                        var dataBytes = Encoding.UTF8.GetBytes(result.Token);

                    var symmetric = SymmetricAlgorithm.Create("TripleDes");
                    symmetric.Mode = CipherMode.ECB;
                    symmetric.Padding = PaddingMode.PKCS7;

                    var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(MelliHelper.GetMerchantKey()), new byte[8]);

                    var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                    var data = new
                    {
                        token = result.Token,
                        SignData = signedData
                    };

                    var ipgUri = string.Format("{0}/api/v0/Advice/Verify", MelliHelper.GetPurchasePage());

                    var res = CallApi<VerifyResultData>(ipgUri, data);
                    if (res != null && res.Result != null)
                    {
                        if (res.Result.ResCode == "0")
                        {
                            result.VerifyResultData = res.Result;
                            res.Result.Succeed = true;
                            ViewBag.Success = res.Result.Description;
                             UpdatePaymentRequest(orderId, Convert.ToInt64(res.Result.SystemTraceNo), res.Result.ResCode, res.Result.Description, DateTime.Now, res.Result.RetrivalRefNo, EnumPaymentStatus.Pay, true);
                             var payment = _paymentDetialService.GetCheckOutPayment(paymentDetail.OrderId);
                             return RedirectToAction("CheckOut", payment);
                            }
                        else
                        {
 UpdatePaymentRequest(orderId, 0, res.Result.ResCode, res.Result.Description, DateTime.Now, "", EnumPaymentStatus.Error, false);
                        var message =
                                "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید" + " - " +
                                res.Result.Description
                            ;

                        TempData["Message"] = message;
                        return RedirectToAction("CheckOut");
                        }
                       

                    }
                    else
                    {
                        UpdatePaymentRequest(orderId, 0, "", "خطا در انجام عملیات", DateTime.Now, "", EnumPaymentStatus.Error, false);
                        var message =
                                "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید"
                            ;

                        TempData["Message"] = message ;
                    }
                    }
                }


            }
            catch (Exception ex)
            {
                UpdatePaymentRequest(orderId, 0, "0", ex.Message, DateTime.Now, "", EnumPaymentStatus.Error, false);
                var message =
                        "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید" + " - " +
                        ex.Message
                    ;

                TempData["Message"] = message;
                return RedirectToAction("CheckOut");
            }

            var payment2 = _paymentDetialService.GetCheckOutPayment(orderId);
            return RedirectToAction("CheckOut", payment2);
        }

        private async Task<bool> UpdatePaymentRequest(long id, long saleReferenceId, string bankCodeReturn, string bankMessageReturn, DateTime datePay, string cardHolderPAN, EnumPaymentStatus status, bool state)
        {
            try
            {
                _orderService.OrderChecked(id);
                if (bankCodeReturn != "43")
                {
                    var payDet = _paymentDetialService.GetOnliyTypePaymentDetail(id);
                    payDet.BankCodeReturn = bankCodeReturn;
                    payDet.BankCodeMessage = bankMessageReturn;
                    payDet.SaleReferenceId = saleReferenceId;
                    payDet.DatePay = datePay;
                    payDet.PrDatePay = datePay.GetMonthPersian();
                    payDet.PaymentStatus = status;
                    payDet.CardHolderPAN = cardHolderPAN;
                    payDet.State = state;
                    _paymentDetialService.UpdatePaymentDetail(payDet);

                    if (status == EnumPaymentStatus.Pay)
                    {
                        _orderService.ConfirmOrder(id);
                        SendOrderToCrm(id);
                        await SendSmsDeliveryCode(id);
                        await SendSmsConfirm(id);

                    }
                    else
                    {
                        var orderDet = _orderService.GetOrderDetailsForCrm(id);
                        var order = _orderService.GetOrderById(id);
                        foreach (var det in orderDet)
                        {
                            AddProductToCartAfterPay(det.VariantId, order.UserId, det.Count);
                            var varient = _variantService.GetVariantsId(det.VariantId);
                            if (varient != null)
                            {
                                if (order.IsColleauge)
                                {
                                    varient.Count += det.Count;

                                }
                                else
                                {
                                    varient.Count += det.Count;

                                }
                                _variantService.EditVariant(varient);
                            }
                        }
                        //
                        var creditPay = _paymentDetialService.GetCreditTypePaymentDetail(id);
                        if (creditPay != null)
                        {
                            var user = _userService.GetUserById(order.UserId);
                            if (user.IsCredit)
                            {
                                var tomanPrice = creditPay.Price / 10;
                                user.AcceptPrice += tomanPrice;
                                _userService.EditUser(user);
                            }

                        }
                        //

                    }
                }


                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task SendSmsConfirm(long id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                var products = _orderService.GetOrderProductsByOrderId(id);
                var productName = "";
                foreach (var product in products)
                {
                    productName += " | " + product;
                }

                var price = (order.AmountPayable / 10).ToString("N0");
                //var message = $"نام مشتری: {order.ClientName} " + $" کالا : {productName} مبلغ سفارش:{price} تومان";
                //_smsSender.SendSms("09141800057", message);
                //_smsSender.SendSms("09147777338", message);
                //_smsSender.SendSms("09020093092", message);

                await _smsSender.SendSms(order.ClientTel, "ConfirmBuy", order.ClientName.Replace(" ", "_"), order.Id.ToString(),
                    price);
            }
            catch (Exception e)
            {
            }
        }

        private async Task SendSmsDeliveryCode(long id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                if (order.NeedUseDeliveryCode)
                {

                    await _smsSender.SendSms(order.ClientTel, "InPersonDelivery", order.Id.ToString(), order.InPersonCode);
                }


            }
            catch (Exception e)
            {
            }
        }

        private bool SendOrderToCrm(long orderId)
        {
            try
            {

                CrmImplement crmImplement = new CrmImplement(_crmService);
                var crmOrder = _orderService.GetOrderById(orderId);
                crmOrder.CrmSendCode = CodeGenerator.CrmOrderCodeGenerator();
                _orderService.UpdateOrder(crmOrder);
                var user = _userService.GetUserById(crmOrder.UserId);
                var legal = _userService.GetUserLegalByUserId(crmOrder.UserId);
                var crmOrderDetails = _orderService.GetOrderDetailsForCrm(orderId);
                var token = crmImplement.GetToken(crmOrder.UserId, crmOrder.CrmSendCode);
                var payDet = _paymentDetialService.GetOnliyTypePaymentDetail(orderId);
                var payDetCredit = _paymentDetialService.GetCreditTypePaymentDetail(orderId);
                var pSPDesc = "ندارد";
                try
                {
                    if (payDet != null)
                    {
                        pSPDesc = "کد تراکنش : " + payDet.SaleReferenceId.ToString() + " - ";
                    }
                    pSPDesc += (string.IsNullOrEmpty(crmOrder.ShipmentTitle)
                        ? "ندارد"
                        : crmOrder.ShipmentTitle);
                    if (payDetCredit != null)
                    {
                        pSPDesc += $" - مبلغ {payDetCredit.Price.ToString("N0")} ریال توسط کاربر به صورت اعتباری پردات شده است";
                    }

                    if (user.IsColleague)
                    {
                        pSPDesc += " - **فروش ویژه همکار**";
                    }

                    if (crmOrder.NeedUseDeliveryCode == true)
                    {
                        pSPDesc += " - کد تحویل : " + crmOrder.InPersonCode;

                    }
                }
                catch (Exception e)
                {

                }

                if (!string.IsNullOrEmpty(token))
                {
                    var rseInit = true;
                    foreach (var orderDetail in crmOrderDetails)
                    {
                        try
                        {
                            var provinceCity = crmOrder.ClientAddress.Split(",");
                            InitializeFactor factor = new InitializeFactor
                            {
                                token = token,
                                pSPDate = DateTime.Now,
                                pSPReference = (int)crmOrder.CrmSendCode,
                                pPrjCode = "0",
                                pRCVRName = string.IsNullOrEmpty(crmOrder.RecipientName) ? "ندارد" : crmOrder.RecipientName,
                                pRCVRPhone = crmOrder.RecipientTel,
                                pRCVRPostalCode = crmOrder.RecipientPostalCode,
                                pRCVRAddress = string.IsNullOrEmpty(crmOrder.RecipientAddress) ? "ندارد" : crmOrder.RecipientAddress.Replace("\r\n", ""),
                                pCustNationalCode = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.ShenaseMeli) ? legal.ShenaseMeli : user.NatioalCode,
                                pCustName = user.IsHoghughi ? "-" : crmOrder.ClientName,
                                pCustPhone = crmOrder.ClientTel,
                                pCustAddress = string.IsNullOrEmpty(crmOrder.ClientAddress) ? "ندارد" : crmOrder.ClientAddress.Replace("\r\n", ""),
                                pCustProvince = provinceCity[0].Replace("\r\n", ""),
                                pCustCity = provinceCity[1].Replace("\r\n", ""),
                                pCustPostalCode = string.IsNullOrEmpty(crmOrder.ClientPostalCode) ? "ندارد" : crmOrder.ClientPostalCode,
                                pDiscount = 0,
                                pCostAmount1 = crmOrder.ShipmentPrice,
                                pCostAmount2 = 0,
                                pCostAmount3 = 0,
                                pCostAmount4 = 0,
                                pSPDesc = pSPDesc,
                                pMerchandiseCode = orderDetail.ProductSpecCode.PersianToEnglish(),
                                pAmount = orderDetail.Count,
                                pUnitPrice = (float)Math.Round(orderDetail.Price / 1.09, 0),
                                pCashValue = crmOrder.AmountPayable,
                                pCashDesc = "ندارد",
                                PMerchDesc = orderDetail.ProductColor + " , " + orderDetail.ProductGaranty,
                                pRemainder = 0,
                                pQId = orderDetail.OrderId,
                                //
                                pCustFamily = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CompanyName) ? legal.CompanyName : "-",
                                pCustType = user.IsHoghughi ? 6 : 1,
                                pCDesc = "-",
                                pSubscriptionNo = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodePostiNeshaniMahaleKar) ? legal.CodePostiNeshaniMahaleKar : "-",
                                pGroupName = "-",
                                pAddress2 = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.NeshaniMahaleKar) ? legal.NeshaniMahaleKar : "-",
                                pCEOEmail = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.TelephoneSabet) ? legal.TelephoneSabet : "-",
                                pWhatsapp = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.TarikhTasis) ? legal.TarikhTasis : "-",
                                pTelegram = "-",
                                pLinkdin = "-",
                                pFacebook = "-",
                                pManuFactoryNo = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodeNaghshTajer) ? legal.CodeNaghshTajer : "-",
                                pWebSite = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodeEghtesadi) ? legal.CodeEghtesadi : "-",
                            };
                            var res = crmImplement.InitializeFactorInfo(factor, crmOrder.UserId, crmOrder.Id, crmOrder.CrmSendCode);
                            if (!res)
                            {
                                rseInit = res;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            return false;

                        }

                    }

                    if (rseInit)
                    {
                        ValidityAndImportInvoce validityAndImportInvoce = new ValidityAndImportInvoce
                        {
                            token = token,
                            pSPReference = (int)crmOrder.CrmSendCode,
                            pQId = crmOrder.Id,
                        };
                        var validityAndImportInvoceRes = crmImplement.ValidityAndImportInvoce(validityAndImportInvoce, crmOrder.UserId, crmOrder.Id, crmOrder.CrmSendCode);
                        if (validityAndImportInvoceRes > 0)
                        {
                            _orderService.SendOrderToCrm(orderId, validityAndImportInvoceRes);
                            return true;

                        }
                        return false;

                    }
                    return false;

                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return result.Result;
                }
                return default(T);
            }
        }
    }
}