using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EShop.Admin.ViewModels;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Sender;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ColleagueOrderController : BaseAdminController
    {
        private IUserService _userService;
        private IAddressService _addressService;
        private IShipmentService _shipmentService;
        private IVariantService _variantService;
        private ICartService _cartService;
        private IOrderService _orderService;
        private IPaymentDetialService _paymentDetialService;
        private ISmsSender _smsSender;
 
        public ColleagueOrderController(IUserService userService, IAddressService addressService, IShipmentService shipmentService, IVariantService variantService, ICartService cartService, IOrderService orderService, IPaymentDetialService paymentDetialService, ISmsSender smsSender)
        {
            _userService = userService;
            _addressService = addressService;
            _shipmentService = shipmentService;
            _variantService = variantService;
            _cartService = cartService;
            _orderService = orderService;
            _paymentDetialService = paymentDetialService;
            _smsSender = smsSender;
        }

        public IActionResult RegisterCart()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCart(ColleagueOrderCart cartOrder)
        {
            var textInsurance = cartOrder.insurance == 0 ? "بدون بیمه" : cartOrder.insurance + "درصد بیمه";

            var colleagueCart = cartOrder.Details.Where(c => c.Count > 0).ToList();



            int concurenncycount = 0;
            long orderId = 0;
            if (colleagueCart.Count > 0)
            {
                RemoveExistCart(cartOrder.UserId);
                CreateCart(cartOrder);
                foreach (var cartDetail in colleagueCart)
                {
                    AddProductToCart(cartOrder.UserId, cartDetail.VariantId, cartDetail.Count);

                }

            start:
                var cartdetail = _cartService.GetCartDetialForSubmitOrder((int)cartOrder.UserId,EnumTypeSystem.Farnaa);

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
                                    TempData["ErrorMessage"] = "عدم هماهنگی موجودی محصول";

                                    return View(cartOrder);
                                }

                                if (item.SpecialPrice != item.CartPrice)
                                {
                                    scope.Dispose();
                                    TempData["ErrorMessage"] = "عدم هماهنگی قیمت محصول";

                                    return View(cartOrder);
                                }
                            }
                            else
                            {
                                scope.Dispose();
                                TempData["ErrorMessage"] = "عدم موجودی محصول";

                                return View(cartOrder);
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
                                item.variant.Count -= item.CartCount;


                            }

                        }

                        if (!_variantService.UpdateVariantAndVariantPromotion(cartdetail.Select(c => c.variant).ToList(),
                            cartdetail.Where(c => c.VariantPromotion != null).Select(c => c.VariantPromotion).ToList()))
                        {
                            if (concurenncycount > 1)
                            {
                                scope.Dispose();
                                TempData["ErrorMessage"] = "خطا در عملیات";

                                return View(cartOrder);
                            }
                            else
                            {
                                scope.Dispose();
                                concurenncycount += 1;
                                goto start;
                            }
                        }

                        _cartService.RemoveCartDetialList(tempcartdetial);
                        var notactivecartdetail = _cartService.GetNotActiveCartDetail((int)cartOrder.UserId);
                        foreach (var item in notactivecartdetail)
                        {
                            item.IsActiveCart = true;
                        }
                        _cartService.ListUpdateCartDetial(notactivecartdetail);
                        #endregion

                        #region saveorder
                        var user = _userService.GetUserById(cartOrder.UserId);
                        if (string.IsNullOrEmpty(user.NatioalCode))
                        {
                            scope.Dispose();
                            TempData["ErrorMessage"] = "پروفایل کاربر تکمیل نشده است";

                            return View(cartOrder);
                        }
                        var cart = _cartService.GetCartByUserId((int)cartOrder.UserId);
                        if (cart.AddressId == null || cart.AddressId == 0)
                        {

                            scope.Dispose();
                            TempData["ErrorMessage"] = "آدرس انتخاب نشده است";

                            return View(cartOrder);
                        }

                        if (cart.ShipmentId == null)
                        {

                            scope.Dispose();
                            TempData["ErrorMessage"] = "روش ارسال انتخاب نشده است";

                            return View(cartOrder);
                        }

                        var shipmet = _shipmentService.GetShipmentById(cart.ShipmentId.Value);
                        if (shipmet == null)
                        {
                            scope.Dispose();
                            TempData["ErrorMessage"] = "خطا در انجام عملیات";

                            return View(cartOrder);
                        }

                        var address = _addressService.GetAddressForOrder(cart.AddressId);
                        var clientAddress = _addressService.GetUserClientAddress((int)cartOrder.UserId);
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
                            UserId = cartOrder.UserId,
                            IsColleauge = true,
                            IsColleaugeOrder = true,
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
                        long sumorderamount = 0;
                        long sumdiscountamount = 0;


                        long tempsum = 0;


                        foreach (var item in cartdetail)
                        {
                            tempsum += item.CartCount * item.SpecialPrice;
                            sumdiscountamount += (item.CartCount * (item.MainPrice - item.SpecialPrice)) * 10;
                            var t = (item.CartCount * item.SpecialPrice) * 10;
                            sumorderamount = sumorderamount + t;
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



                        sumorderamount += shippingPrice * 10;



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
                                Type = (EnumPaymentType)cartOrder.Type,
                                PaymentStatus = EnumPaymentStatus.Wait,
                                TypeBank = 200
                            });
                            _paymentDetialService.AddRangePaymentDetial(paymentDetails);
                            order.AmountPayable = sumorderamount;
                            order.OrderStatus = EnumOrderStatus.WaitPay;
                            order.SumAmount = sumorderamount + sumdiscountamount;
                            _orderService.UpdateOrder(order);




                            orderId = order.Id;


                            scope.Complete();



                        }
                        else
                        {
                            scope.Dispose();
                            TempData["ErrorMessage"] = "خطا در انجام عملیات";

                            return View(cartOrder);
                        }
                        #endregion
                    }

                    if (orderId != 0)
                   await SendSmsPreFactor(orderId);

                    return RedirectToAction("ListTempOrder");

                }
                else
                {

                    return View(cartOrder);
                }
            }
            else
            {
                return View(cartOrder);
            }
        }
        private async Task SendSmsPreFactor(long id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);


                var price = (order.AmountPayable / 10).ToString("N0");

                await _smsSender.SendSms(order.ClientTel, "RegisterPreFactor", order.ClientName.Replace(" ", "_"), order.Id.ToString(),
                    price, "09960796016");
            }
            catch (Exception e)
            {
            }
        }
        public IActionResult RejectOrder(long id)
        {
            var order = _orderService.GetOrderById(id);
            order.OrderStatus = EnumOrderStatus.NotPay;
            _orderService.UpdateOrder(order);
            //
            var orderDet = _orderService.GetOrderDetailsForCrm(id);
             foreach (var det in orderDet)
            {
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
            return RedirectToAction("ListTempOrder");

        }
        [HttpPost]
        public IActionResult ConfirmOrder(ConfirmColleagueOrderViewModel model)
        {
            var order = _orderService.GetOrderById(model.OrderId);
            var payment = _paymentDetialService.GetPaymentDetailByType(model.OrderId, EnumPaymentType.CashColleague);
            //
            if (payment != null)
            {
                payment.SaleReferenceId = model.SaleReferenceId;
                payment.CardHolderPAN = model.CardHolderPAN;
                payment.BankCodeMessage = model.BankCodeMessage;
                _paymentDetialService.UpdatePaymentDetail(payment);
            }

            if (order != null)
            {
                order.OrderStatus = EnumOrderStatus.Pay;
                _orderService.UpdateOrder(order);
            }

            return RedirectToAction("ListTempOrder");

        }

        private void RemoveExistCart(long userId)
        {
            var userCart = _cartService.GetCartByUserId((int)userId);
            if (userCart != null)
            {
                var res2 = _cartService.RemoveCartAndCartDetail(userCart);
            }
        }

        private void AddProductToCart(long userId, long variantId, int count)
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
                cartDetail = _cartService.GetCartDetailByCartIdAndVariantId(cartid, (int)variantId);
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


            var variant = _variantService.GetMinPriceAndCountByVariantIdAndUserType((int)variantId, (int)userId);
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
                        VariantId = variantId,
                        Count = count,
                        Price = variant.Item1,
                        IsActiveCart = true,
                    };

                    _cartService.AddCartDetail(detail);
                }
            }

        }

        private void CreateCart(ColleagueOrderCart orderCart)
        {
            var cart = _cartService.GetCartByUserId((int)orderCart.UserId);
            if (cart != null)
            {
                cart.UpdateDate = DateTime.Now;
                cart.AddressId = orderCart.AddressId;
                cart.ShipmentId = orderCart.ShippingId;
                _cartService.UpdateCart(cart);
            }
            else
            {
                cart = new Cart
                {
                    UserId = orderCart.UserId,
                    AddressId = orderCart.AddressId,
                    ShipmentId = orderCart.ShippingId,
                    UpdateDate = DateTime.Now
                };
                _cartService.AddCart(cart);
            }
        }


        public IActionResult ListTempOrder(UserOrderSearchAdmin search)
        {
            if (search == null)
                search = new UserOrderSearchAdmin();
            //
            search.OrderStatus = EnumOrderStatus.WaitPay;
            search.IsColleaugeOrder = true;
            //

            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            var res = _orderService.GetListUserOrderAdmin(search, true);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult ListConfirmOrder(UserOrderSearchAdmin search)
        {
            if (search == null)
                search = new UserOrderSearchAdmin();
            //
            search.OrderStatus = EnumOrderStatus.Pay;
            search.IsColleaugeOrder = true;
            //

            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            var res = _orderService.GetListUserOrderAdmin(search, true);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult ListRejectOrder(UserOrderSearchAdmin search)
        {
            if (search == null)
                search = new UserOrderSearchAdmin();
            //
            search.OrderStatus = EnumOrderStatus.NotPay;
            search.IsColleaugeOrder = true;
            //

            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            var res = _orderService.GetListUserOrderAdmin(search, true);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        [Produces("application/json")]
        [Authorize]
        [IgnoreAntiforgeryToken]

        public ActionResult GetListColleaugeUser(IFormCollection frm)
        {

            var name = frm.FirstOrDefault(c => c.Key.Equals("data[q]")).Value.ToString().Replace("ی", "ي");
            var users = _userService.GetListColleaugeUser(name);
            var data = users.Select(p => new SelectListItem
            {
                Selected = false,
                Value = p.Id.ToString(),
                Text = p.Name + " " + p.Family + " - " + p.Phone + " - " + p.NatioalCode
            }).ToList();

            data.Insert(0, new SelectListItem
            {
                Text = "انتخاب کنید",
                Selected = true,
                Value = "0"
            });
            return Json(new { q = name, results = data });

        }
        [Produces("application/json")]
        [Authorize]
        [IgnoreAntiforgeryToken]

        public ActionResult GetListProductVariants(IFormCollection frm)
        {

            var name = frm.FirstOrDefault(c => c.Key.Equals("data[q]")).Value.ToString().Replace("ی", "ي");
            var variants = _variantService.GetListVariants(name).Where(c => c.Count > 0 && c.PriceColleague > 0);
            var data = variants.Select(p => new SelectListItem
            {
                Selected = false,
                Value = p.VariantId.ToString(),
                Text = p.ProductFaTitle + " " + p.ProductEnTitle + " - " + p.Guarantee + " - " + p.ProductOption + " - " + p.PriceColleague.ToString("N0"),
            }).ToList();

            data.Insert(0, new SelectListItem
            {
                Text = "انتخاب کنید",
                Selected = true,
                Value = "0"
            });
            return Json(new { q = name, results = data });

        }

        [IgnoreAntiforgeryToken]
        public JsonResult GetUserAddresses(int id)
        {
            ViewBag.provincelist = _addressService.GetProvince();
            AddressListViewModel model = new AddressListViewModel
            {
                AddressList = _addressService.GetUserAddresses(id)
            };
            return Json(JsonConvert.SerializeObject(model));
        }

        [IgnoreAntiforgeryToken]
        public JsonResult GetProductDetail(int id)
        {
            var res = _variantService.GetVariantsDetailById(id);
            return Json(res);
        }

        [IgnoreAntiforgeryToken]
        public JsonResult GetShipmentByAddress([FromRoute] int id)
        {
             var shipments = _shipmentService.GetShipmentColleagueByAddress((long)id);
            if (shipments.Any(s => s.ShipmentId == 3))
                shipments.RemoveAll(s => s.Title == "تیپاکس");

            return Json(JsonConvert.SerializeObject(shipments));
        }
        [IgnoreAntiforgeryToken]
        public JsonResult GetShipmentDetail(int id, int count)
        {
            var shipment = _shipmentService.GetShipmentById(id);
            if(shipment != null)
            {
                var price = shipment.Price;
                if (count > 1)
                {
                    var extPrice = (count - 1) * shipment.PricePerAddProduct;
                    price += extPrice;
                }

                return Json(JsonConvert.SerializeObject(new ShipmentViewModel(shipment.Title, price)));
            }
            else
            {
               
                return Json(JsonConvert.SerializeObject(new ShipmentViewModel("", 0)));
            }
        
        }
    }
}