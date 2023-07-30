using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Cart;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Order
{
    public class OrderService : BaseService<DataLayer.Entities.Order.Order>, IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IPaymentDetialService _paymentDetialService;

        public OrderService(ApplicationDbContext context, IUserService userService, IPaymentDetialService paymentDetialService) : base(context)
        {
            _context = context;
            _userService = userService;
            _paymentDetialService = paymentDetialService;
        }

        public bool UserExistOrder(int userid)
        {
            return _context.Orders.Any(o => o.UserId == userid);
        }

        public int GetUserUseDiscountCodeCount(int userid, long discountcodeid)
        {
            return _context.Orders.Count(c => c.UserId == userid && c.PaymentDetails.Any(c => c.DiscountCodeId == discountcodeid));
        }

        public long AddOrder(DataLayer.Entities.Order.Order order)
        {
            order = order.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public void UpdateOrder(DataLayer.Entities.Order.Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
        }

        public DataLayer.Entities.Order.Order GetOrderById(long orderid)
        {
            return _context.Orders.Include(c => c.OrderDetails)
                 .SingleOrDefault(c => c.Id == orderid);
        }


        public List<UserOrder> GetListUserOrder(long userId)
        {
            try
            {
                var quary = _context.Orders
                               .Where(c => c.UserId == userId && c.OrderStatus != EnumOrderStatus.NotPay)
                               .OrderByDescending(c => c.Id)
                               .Include(c => c.PaymentDetails)
                               .Include(c => c.OrderDetails)
                               .ThenInclude(c => c.Variant)
                               .ThenInclude(c => c.Product)
                               .Select(c => new UserOrder
                               {
                                   OrderId = c.Id,
                                   UserId = c.UserId,
                                   PaymentDetailId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().Id,
                                   RecipientName = c.RecipientName,
                                   PaymentStatus = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PaymentStatus,
                                   PrDatePay = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PrDatePay,
                                   AmountPayable = c.AmountPayable,
                                   Discount = c.SumAmount - c.AmountPayable,
                                   OrderStatus = c.OrderStatus,
                                   RecipientAddress = c.RecipientAddress,
                                   RecipientPostalCode = c.RecipientPostalCode,
                                   RecipientTel = c.RecipientTel,
                                   SaleReferenceId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().SaleReferenceId,
                                   ShipmentPrice = c.ShipmentPrice,
                                   ShipmentTitle = c.ShipmentTitle,
                                   SumAmount = c.SumAmount,
                                   TrackingCodePost = c.TrackingCodePost,
                                   OrderDetails = new List<UserOrderDetail>(c.OrderDetails.Select(o => new UserOrderDetail
                                   {
                                       Price = o.Price,
                                       Count = o.Count,
                                       Discount = o.Discount,
                                       Color = o.Variant.productOption.Name,
                                       ColorValue = o.Variant.productOption.Value,
                                       FaTitle = o.Variant.Product.FaTitle,
                                       Guarantee = o.Variant.Guarantee.Title,
                                       Image = o.Variant.Product.ImgName,
                                       OrderId = o.OrderId,
                                       ProductId = o.Variant.ProductId,
                                       SumPrice = o.SumPrice,
                                       SumPriceAfterDiscount = o.SumPriceAfterDiscount,
                                       VariantId = o.VariantId
                                   }
                                   ))
                               });
                return quary.ToList();
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public UserOrder GetOrderForPrint(long id, long userId)
        {
            try
            {
                var quary = _context.Orders
                               .Where(c => c.Id == id && c.UserId == userId)
                                .Include(c => c.PaymentDetails)
                               .Include(c => c.OrderDetails)
                               .ThenInclude(c => c.Variant)
                               .ThenInclude(c => c.Product)
                               .Select(c => new UserOrder
                               {
                                   OrderId = c.Id,
                                   UserId = c.UserId,
                                   PaymentDetailId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().Id,
                                   RecipientName = c.RecipientName,
                                   PaymentStatus = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PaymentStatus,
                                   PrDatePay = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PrDatePay,
                                   AmountPayable = c.AmountPayable,
                                   Discount = c.SumAmount - c.AmountPayable,
                                   OrderStatus = c.OrderStatus,
                                   RecipientAddress = c.RecipientAddress,
                                   RecipientPostalCode = c.RecipientPostalCode,
                                   RecipientTel = c.RecipientTel,
                                   SaleReferenceId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().SaleReferenceId,
                                   ShipmentPrice = c.ShipmentPrice,
                                   ShipmentTitle = c.ShipmentTitle,
                                   SumAmount = c.SumAmount,
                                   TrackingCodePost = c.TrackingCodePost,
                                   ClientAddress = c.ClientAddress,
                                   ClientName = c.ClientName,
                                   ClientNatioalCode = c.ClientNatioalCode,
                                   ClientPostalCode = c.ClientPostalCode,
                                   ClientTel = c.ClientTel,
                                   OrderDetails = new List<UserOrderDetail>(c.OrderDetails.Select(o => new UserOrderDetail
                                   {
                                       Price = o.Price,
                                       Count = o.Count,
                                       Discount = o.Discount,
                                       Color = o.Variant.productOption.Name,
                                       ColorValue = o.Variant.productOption.Value,
                                       FaTitle = o.Variant.Product.FaTitle,
                                       Guarantee = o.Variant.Guarantee.Title,
                                       Image = o.Variant.Product.ImgName,
                                       OrderId = o.OrderId,
                                       ProductId = o.Variant.ProductId,
                                       SumPrice = o.SumPrice,
                                       SumPriceAfterDiscount = o.SumPriceAfterDiscount,
                                       VariantId = o.VariantId
                                   }
                                   ))
                               });
                return quary.FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public Tuple<int, List<UserOrderAdmin>> GetListUserOrderAdmin(UserOrderSearchAdmin search, bool? isColleague = null)
        {
            try
            {
                var quary = _context.Orders
                                .OrderByDescending(c => c.Id)
                               .Include(c => c.PaymentDetails)
                               .Include(c => c.OrderDetails)
                               .ThenInclude(c => c.Variant)
                               .ThenInclude(c => c.Product)
                                                                .Select(c => new UserOrderAdmin
                                                                {
                                                                    CreateDate = c.CreateDate,

                                                                    OrderId = c.Id,
                                                                    UserId = c.UserId,
                                                                    PaymentDetailId = c.PaymentDetails.OrderByDescending(detail => detail.Id).FirstOrDefault().Id,
                                                                    RecipientName = c.RecipientName,
                                                                    PaymentStatus = c.PaymentDetails.OrderByDescending(detail => detail.Id).FirstOrDefault().PaymentStatus,
                                                                    PrDate = c.CreateDate.GetMonthPersian(),
                                                                    PrDatePay = c.PaymentDetails.OrderByDescending(detail => detail.Id).FirstOrDefault().PrDatePay,
                                                                    DatePay = c.PaymentDetails.OrderByDescending(detail => detail.Id).FirstOrDefault().DatePay,
                                                                    AmountPayable = c.AmountPayable,
                                                                    Discount = c.SumAmount - c.AmountPayable,
                                                                    OrderStatus = c.OrderStatus,
                                                                    RecipientAddress = c.RecipientAddress,
                                                                    RecipientPostalCode = c.RecipientPostalCode,
                                                                    RecipientTel = c.RecipientTel,
                                                                    SaleReferenceId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().SaleReferenceId,
                                                                    RefId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().RefId,
                                                                    BankCodeReturn = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().BankCodeReturn,
                                                                    CardHolderPAN = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().CardHolderPAN,
                                                                    BankCodeMessage = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().BankCodeMessage,
                                                                    TypePayment = (EnumPaymentTypeColleaugeCart)c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().Type,
                                                                    ShipmentPrice = c.ShipmentPrice,
                                                                    ShipmentTitle = c.ShipmentTitle,
                                                                    SumAmount = c.SumAmount,
                                                                    TrackingCodePost = c.TrackingCodePost,
                                                                    ClientName = c.ClientName,
                                                                    ClientNatioalCode = c.ClientNatioalCode,
                                                                    ClientTel = c.ClientTel,
                                                                    IsSendToTadbir = c.IsSendToTadbir,
                                                                    CrmValidationItem = c.CrmValidationItem,
                                                                    FactorNoTadbir = c.FactorNoTadbir,
                                                                    InPersonCode = c.InPersonCode,
                                                                    DeliveryDate = c.DeliveryDate,
                                                                    PrDeliveryDate = c.PrDeliveryDate,
                                                                    DeliveryTime = c.DeliveryTime,
                                                                    NeedUseDeliveryCode = c.NeedUseDeliveryCode,
                                                                    IsColleauge = c.IsColleauge,
                                                                    IsColleaugeOrder = c.IsColleaugeOrder,
                                                                    OrderDetails = new List<UserOrderDetail>(c.OrderDetails.Select(o => new UserOrderDetail
                                                                    {
                                                                        Price = o.Price,
                                                                        Count = o.Count,
                                                                        Discount = o.Discount,
                                                                        Color = o.Variant.productOption.Name,
                                                                        ColorValue = o.Variant.productOption.Value,
                                                                        FaTitle = o.Variant.Product.FaTitle,
                                                                        Guarantee = o.Variant.Guarantee.Title,
                                                                        Image = o.Variant.Product.ImgName,
                                                                        OrderId = o.OrderId,
                                                                        ProductId = o.Variant.ProductId,
                                                                        SumPrice = o.SumPrice,
                                                                        SumPriceAfterDiscount = o.SumPriceAfterDiscount,
                                                                        VariantId = o.VariantId,

                                                                    }
                                   ))
                                                                });

                if (search.OrderId != null)
                    quary = quary.Where(c => c.OrderId == search.OrderId);
                if (!string.IsNullOrEmpty(search.ClientName))
                    quary = quary.Where(c => c.ClientName.Contains(search.ClientName));
                if (!string.IsNullOrEmpty(search.ClientTel))
                    quary = quary.Where(c => c.ClientTel.Contains(search.ClientTel));
                if (!string.IsNullOrEmpty(search.Shipment))
                    quary = quary.Where(c => c.ShipmentTitle.Contains(search.Shipment));
                if (!string.IsNullOrEmpty(search.ClientNatioalCode))
                    quary = quary.Where(c => c.ClientNatioalCode.Contains(search.ClientNatioalCode));
                if (search.ConfairmAnbar == null && search.WaitConfairmAnbar == null)
                {
                    if (search.OrderStatus != null)
                        quary = quary.Where(c => c.OrderStatus == search.OrderStatus);

                    if (search.SendTadbir != null)
                    {
                        var state = search.SendTadbir == EnumYesNo.Yes;
                        quary = quary.Where(c => c.IsSendToTadbir == state);
                    }
                    if (search.Delivered == null)
                        if (isColleague != null)
                            quary = quary.Where(c => c.IsColleauge == isColleague);
                }
                else
                {
                    if (search.WaitConfairmAnbar == true)
                        quary = quary.Where(c => c.OrderStatus == EnumOrderStatus.SendToAnbar);
                    if (search.ConfairmAnbar == true)
                        quary = quary.Where(c => c.OrderStatus == EnumOrderStatus.OutAnbar || c.OrderStatus == EnumOrderStatus.SendPost);

                }
                if (search.Delivered == true)
                {
                    quary = quary.Where(c => c.DeliveryDate != null && c.NeedUseDeliveryCode);

                }
                else if (search.Delivered == false)
                {
                    quary = quary.Where(c => c.DeliveryDate == null && c.NeedUseDeliveryCode);

                }

                if (search.IsColleaugeOrder != null)
                    quary = quary.Where(c => c.IsColleaugeOrder == true);





                if (search.PSDate != null && search.PEDate != null)
                {

                    var sDate = search.PSDate.PersianToEnglish().ConvertShamsiToMiladi();
                    var eDate = search.PEDate.PersianToEnglish().ConvertShamsiToMiladi();

                    quary = quary.Where(c => c.CreateDate > sDate && c.CreateDate < eDate);
                }
                else if (search.PSDate != null)
                {

                    var sDate = search.PSDate.PersianToEnglish().ConvertShamsiToMiladi();

                    quary = quary.Where(c => c.CreateDate > sDate);
                }
                else if (search.PEDate != null)
                {
                    var eDate = search.PEDate.PersianToEnglish().ConvertShamsiToMiladi();

                    quary = quary.Where(c => c.CreateDate < eDate);
                }


                int skip = (search.PageNumber.Value - 1) * 30;
                return Tuple.Create(0, quary.ToList());

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public Tuple<int, List<PaymentDetail>> GetListUserPaymentAdmin(string RefId = "", string PSDate = "", string PEDate = "", long? OrderId = null,
            int PageNumber = 1, int count = 15)
        {
            int skip = (PageNumber - 1) * count;
            var quary = _context.PaymentDetails
                .OrderByDescending(c => c.Id).AsQueryable();


            if (OrderId != null)
                quary = quary.Where(c => c.OrderId == OrderId.Value);
            if (!string.IsNullOrEmpty(RefId))
                quary = quary.Where(c => c.RefId.Contains(RefId));

            if (PSDate != null && PEDate != null)
            {

                var sDate = PSDate.PersianToEnglish().ConvertShamsiToMiladi();
                var eDate = PEDate.PersianToEnglish().ConvertShamsiToMiladi();

                quary = quary.Where(c => c.CreateDate > sDate && c.CreateDate < eDate);
            }
            else if (PSDate != null)
            {

                var sDate = PSDate.PersianToEnglish().ConvertShamsiToMiladi();

                quary = quary.Where(c => c.CreateDate > sDate);
            }
            else if (PEDate != null)
            {
                var eDate = PEDate.PersianToEnglish().ConvertShamsiToMiladi();

                quary = quary.Where(c => c.CreateDate < eDate);
            }
            return Tuple.Create(quary.Count(), quary.Skip(skip).Take(count).ToList());
        }


        public bool ConfirmOrder(long id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                order.OrderStatus = EnumOrderStatus.InProccess;
                _context.Update(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool ConfirmOrderAnbar(long id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                order.OrderStatus = EnumOrderStatus.OutAnbar;
                _context.Update(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<PaymentDetail> GetPaymentDetailsForAdmin()
        {
            var quary = _context.PaymentDetails.OrderByDescending(c => c.Id);
            return quary.ToList();
        }

        public List<OrderDetailForCrmViewModel> GetOrderDetailsForCrm(long orderid)
        {
            var quary = _context.OrderDetails.Where(c => c.OrderId == orderid)
                .Include("Variant.Product")
                .Include("Variant.productOption")
                .Include("Variant.Guarantee")
                .Select(c => new OrderDetailForCrmViewModel
                {
                    Price = c.Price,
                    Count = c.Count,
                    SumPriceAfterDiscount = c.SumPriceAfterDiscount,
                    OrderId = c.OrderId,
                    Discount = c.Discount,
                    DiscountType = c.DiscountType,
                    ProductColor = c.Variant.productOption.Name,
                    ProductGaranty = c.Variant.Guarantee.Title,
                    ProductSpecCode = c.Variant.Product.SpecCode,
                    StorePlace = c.StorePlace,
                    SumPrice = c.SumPrice,
                    UnitDiscount = c.UnitDiscount,
                    VariantId = c.VariantId,
                    ProductId = c.Variant.ProductId
                });
            return quary.ToList();
        }

        public void SendOrderToCrm(long orderid, int crmValidationItem)
        {
            var order = _context.Orders.Find(orderid);
            order.IsSendToTadbir = true;
            order.CrmValidationItem = crmValidationItem;
            order.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(order);
            _context.SaveChanges();
        }

        public DataLayer.Entities.Order.Order GetOrderAndDetailByOrderId(long id)
        {
            return _context.Orders.Where(c => c.Id == id).Include(c => c.OrderDetails).FirstOrDefault();
        }

        public List<string> GetOrderProductsByOrderId(long id)
        {
            List<string> res = _context.OrderDetails.Where(c => c.OrderId == id).Include(c => c.Variant)
                .ThenInclude(c => c.Product).Select(c => c.Variant.Product.FaTitle).ToList();
            return res;
        }

        public List<OrderLimit> GetListOrderLimit()
        {
            return _context.OrderLimits.Include(c => c.Category).ToList();
        }

        public bool CreateOrderLimit(OrderLimit orderLimit)
        {
            try
            {
                var exist = _context.OrderLimits.FirstOrDefault(c =>
                    c.CategoryId == orderLimit.CategoryId && c.LimitType == orderLimit.LimitType);
                if (exist != null)
                    return false;


                orderLimit.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(orderLimit);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditOrderLimit(OrderLimit orderLimit)
        {
            try
            {
                var exist = _context.OrderLimits.Count(c =>
                    c.CategoryId == orderLimit.CategoryId && c.LimitType == orderLimit.LimitType);
                if (exist > 1)
                    return false;

                orderLimit.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(orderLimit);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteOrderLimit(long id)
        {
            try
            {
                var orderLimit = GetOrderLimitById(id);
                orderLimit.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(orderLimit);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public OrderLimit GetOrderLimitById(long id)
        {
            return _context.OrderLimits.Find(id);
        }

        public bool IsUserLimit(long userid, long orderId)
        {

            try
            {
                var orderDetails = _context.OrderDetails
                    .Include(c => c.Order)
                    .Include(c => c.Variant)
                    .ThenInclude(c => c.Product)
                    .Where(c => c.OrderId == orderId).ToList();
                var lstCat = orderDetails.Select(c => c.Variant.Product.CategoryId).ToList();

                foreach (var catId in lstCat)
                {

                    var orderLimits = _context.OrderLimits.Where(c => c.CategoryId == catId).ToList();
                    if (orderLimits.Count > 0)
                    {
                        var factorLimit = orderLimits.FirstOrDefault(c => c.LimitType == EnumOrderLimitType.Factor);
                        var dayLimit = orderLimits.FirstOrDefault(c => c.LimitType == EnumOrderLimitType.Day);
                        var hourLimit = orderLimits.FirstOrDefault(c => c.LimitType == EnumOrderLimitType.Hour);
                        var factorCatPrice = orderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                            .Sum(c => c.SumPriceAfterDiscount);
                        var factorCatCount = orderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                            .Sum(c => c.Count);

                        if (factorLimit != null)
                        {
                            if (factorLimit.Price != null)
                            {
                                if (factorCatPrice > factorLimit.Price)
                                    return true;
                            }

                            if (factorLimit.Count != null)
                            {
                                if (factorCatCount > factorLimit.Count)
                                    return true;
                            }

                        }

                        if (dayLimit?.Value != null)
                        {


                            var day = DateTime.Now.AddDays(-1 * dayLimit.Value.Value);
                            var userOrderDetails = _context.OrderDetails
                                .Include(c => c.Order)
                                .Include(c => c.Variant)
                                .ThenInclude(c => c.Product)
                                .Where(c => c.Order.OrderStatus != EnumOrderStatus.NotPay && c.CreateDate > day).ToList();
                            var orderCatPrice = userOrderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                                .Sum(c => c.SumPriceAfterDiscount) + factorCatPrice;
                            var orderCatCount = userOrderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                                .Sum(c => c.Count) + factorCatCount;
                            if (dayLimit.Price != null)
                            {
                                if (orderCatPrice > dayLimit.Price)
                                    return true;
                            }

                            if (dayLimit.Count != null)
                            {
                                if (orderCatCount > dayLimit.Count)
                                    return true;
                            }

                        }

                        if (hourLimit?.Value != null)
                        {


                            var day = DateTime.Now.AddHours(-1 * hourLimit.Value.Value);
                            var userOrderDetails = _context.OrderDetails
                                .Include(c => c.Order)
                                .Include(c => c.Variant)
                                .ThenInclude(c => c.Product)
                                .Where(c => c.Order.OrderStatus != EnumOrderStatus.NotPay && c.CreateDate > day).ToList();
                            var orderCatPrice = userOrderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                                .Sum(c => c.SumPriceAfterDiscount) + factorCatPrice;
                            var orderCatCount = userOrderDetails.Where(c => c.Variant.Product.CategoryId == catId)
                                .Sum(c => c.Count) + factorCatCount;
                            if (hourLimit.Price != null)
                            {
                                if (orderCatPrice > hourLimit.Price)
                                    return true;
                            }

                            if (hourLimit.Count != null)
                            {
                                if (orderCatCount > hourLimit.Count)
                                    return true;
                            }

                        }
                    }


                }







                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void OrderChecked(long id)
        {
            var order = _context.Orders.Find(id);
            order.IsCheckPayState = true;
            _context.SaveChanges();
        }

        public void ReturnCountToProductVariant()
        {




        }

        public void ConverPreFactorToFactor(ConvertPreFactorToFactorViewModel model)
        {
            var orders = _context.Orders.Where(c =>
                c.CreateDate >= model.StartDate && c.CreateDate <= model.EndDate && c.FactorNoTadbir == null && c.CrmValidationItem != 0).ToList();

            var updatedOrders = new List<DataLayer.Entities.Order.Order>();

            foreach (var factor in model.PreFactorFactors)
            {
                var order = orders.FirstOrDefault(c => c.CrmValidationItem.ToString() == factor.PreFactorNo);
                if (order != null)
                {
                    order.FactorNoTadbir = factor.FactorNo;
                    order.OrderStatus = EnumOrderStatus.SendToAnbar;
                    order.SetEditDefaultValue(_userService.GetUserId());
                    updatedOrders.Add(order);
                }
            }

            _context.UpdateRange(updatedOrders);
            _context.SaveChanges();


        }

        public List<Tuple<long, string>> ChangeOrderState(ChangeOrderStateViewModel model)
        {
            var orders = _context.Orders.Include(o => o.User).Where(c =>
                c.CreateDate >= model.StartDate && c.CreateDate <= model.EndDate && c.FactorNoTadbir != null && c.ShipmentId == model.ShipmentId).ToList();

            var updatedOrders = new List<DataLayer.Entities.Order.Order>();
            var result = new List<Tuple<long, string>>();

            foreach (var factor in model.FactorNo)
            {
                var order = orders.FirstOrDefault(c => c.FactorNoTadbir == factor);
                if (order != null)
                {
                    if (order.OrderStatus != model.OrderStatus)
                    {
                        result.Add(new Tuple<long, string>(order.Id, order.User.Phone));
                        order.OrderStatus = model.OrderStatus;
                        order.SetEditDefaultValue(_userService.GetUserId());
                        updatedOrders.Add(order);
                    }
                }
            }

            _context.UpdateRange(updatedOrders);
            _context.SaveChanges();
            return result;
        }

        public void UploadPostCode(PostCodeViewModel model)
        {
            var orders = _context.Orders.Where(c =>
                c.CreateDate >= model.StartDate && c.CreateDate <= model.EndDate && c.FactorNoTadbir != null && c.TrackingCodePost == null).ToList();

            var updatedOrders = new List<DataLayer.Entities.Order.Order>();

            foreach (var factor in model.FactorPostModels)
            {
                var order = orders.FirstOrDefault(c => c.FactorNoTadbir == factor.FactorNo);
                if (order != null)
                {
                    order.TrackingCodePost = factor.PostCode;
                    order.OrderStatus = EnumOrderStatus.SendPost;
                    order.SetEditDefaultValue(_userService.GetUserId());
                    updatedOrders.Add(order);
                }
            }

            _context.UpdateRange(updatedOrders);
            _context.SaveChanges();
        }

        public void CheckOrderPaymentVerify(long orderId)
        {
            try
            {


                var order = _context.Orders.Where(c => c.Id == orderId && !c.IsCheckPayState)
                    .Include(c => c.PaymentDetails)
                    .Include(c => c.OrderDetails)
                    .ThenInclude(c => c.Variant).FirstOrDefault();

                if (order != null && order.OrderStatus == EnumOrderStatus.NotPay)
                {
                    var variantCounts = order.OrderDetails.Select(c => new
                    {
                        c.Variant.Id,
                        c.Count
                    }).ToList();
                    var variants = new List<DataLayer.Entities.Variety.Variant>();
                    foreach (var variantCount in variantCounts)
                    {
                        var variant = _context.Variants.Find(variantCount.Id);
                        if (order.IsColleauge)
                        {
                            variant.Count += variantCount.Count;
                        }
                        else
                        {
                            variant.Count += variantCount.Count;
                        }

                        variant.SetEditDefaultValue(_userService.GetUserId());
                        variants.Add(variant);
                    }

                    _context.UpdateRange(variants);
                    _context.SaveChanges();



                    var creditPay = _paymentDetialService.GetCreditTypePaymentDetail(orderId);
                    if (creditPay != null)
                    {
                        var user = _userService.GetUserById(order.UserId);
                        if (user.IsCredit)
                        {
                            var tomanPrice = creditPay.Price / 10;
                            user.AcceptPrice += tomanPrice;
                            _context.Update(user);
                            _context.SaveChanges();
                        }

                    }
                }

                if (order != null)
                {
                    order.IsCheckPayState = true;
                    order.SetEditDefaultValue(_userService.GetUserId());
                    _context.Update(order);
                    _context.SaveChanges();
                }



            }
            catch (Exception e)
            {
            }
        }

        public void AddAsanPardakhtLog(AsanPardakhtLog asanPardakhtLog)
        {
            asanPardakhtLog.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(asanPardakhtLog);
            _context.SaveChanges();
        }

        public bool EditOrder(DataLayer.Entities.Order.Order data)
        {
            try
            {
                data.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(data);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public UserOrder GetOrderForPrintAdmin(long id)
        {
            try
            {
                var quary = _context.Orders
                               .Where(c => c.Id == id)
                                .Include(c => c.PaymentDetails)
                               .Include(c => c.OrderDetails)
                               .ThenInclude(c => c.Variant)
                               .ThenInclude(c => c.Product)
                               .Select(c => new UserOrder
                               {
                                   OrderId = c.Id,
                                   UserId = c.UserId,
                                   PaymentDetailId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().Id,
                                   RecipientName = c.RecipientName,
                                   PaymentStatus = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PaymentStatus,
                                   PrDatePay = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().PrDatePay,
                                   AmountPayable = c.AmountPayable,
                                   Discount = c.SumAmount - c.AmountPayable,
                                   OrderStatus = c.OrderStatus,
                                   RecipientAddress = c.RecipientAddress,
                                   RecipientPostalCode = c.RecipientPostalCode,
                                   RecipientTel = c.RecipientTel,
                                   SaleReferenceId = c.PaymentDetails.OrderByDescending(c => c.Id).FirstOrDefault().SaleReferenceId,
                                   ShipmentPrice = c.ShipmentPrice,
                                   ShipmentTitle = c.ShipmentTitle,
                                   SumAmount = c.SumAmount,
                                   TrackingCodePost = c.TrackingCodePost,
                                   ClientAddress = c.ClientAddress,
                                   ClientName = c.ClientName,
                                   ClientNatioalCode = c.ClientNatioalCode,
                                   ClientPostalCode = c.ClientPostalCode,
                                   ClientTel = c.ClientTel,
                                   OrderDetails = new List<UserOrderDetail>(c.OrderDetails.Select(o => new UserOrderDetail
                                   {
                                       Price = o.Price,
                                       Count = o.Count,
                                       Discount = o.Discount,
                                       Color = o.Variant.productOption.Name,
                                       ColorValue = o.Variant.productOption.Value,
                                       FaTitle = o.Variant.Product.FaTitle,
                                       Guarantee = o.Variant.Guarantee.Title,
                                       Image = o.Variant.Product.ImgName,
                                       OrderId = o.OrderId,
                                       ProductId = o.Variant.ProductId,
                                       SumPrice = o.SumPrice,
                                       SumPriceAfterDiscount = o.SumPriceAfterDiscount,
                                       VariantId = o.VariantId
                                   }
                                   ))
                               });
                return quary.FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public DataLayer.Entities.Order.Order GetOrderAndDetailsById(long id)
        {
            return _context.Orders
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.WarehouseProduct)
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.Variant)
                .ThenInclude(c => c.Product)
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.Variant)
                .ThenInclude(c => c.productOption)
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.Variant)
                .ThenInclude(c => c.Guarantee)
                .SingleOrDefault(c => c.Id == id);
        }

        public bool UpdateOrderDetails(OrderDetail productInOrder)
        {
            try
            {
                productInOrder.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(productInOrder);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public OrderDetail GetOrderDetailsById(long id)
        {
            var res = _context.OrderDetails.Find(id);
            return res;
        }
    }
}
