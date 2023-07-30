using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Order
{
   public class PaymentDetialService : BaseService<PaymentDetail>, IPaymentDetialService
    {
       private readonly ApplicationDbContext _context;
       private readonly IUserService _userService;

        public PaymentDetialService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


       public void AddRangePaymentDetial(List<PaymentDetail> paymentDetails)
        {
            foreach (var paymentDetail in paymentDetails)
            {
                paymentDetail.IsDelete = false;
                paymentDetail.CreateDate = DateTime.Now;
                paymentDetail.LastUpdateDate = DateTime.Now;
                
             }
            _context.AddRange(paymentDetails);
            _context.SaveChanges();
        }

        public PaymentDetail GetOnliyTypePaymentDetail(long orderid)
        {
            return _context.PaymentDetails.FirstOrDefault(p => p.OrderId == orderid && p.Type == EnumPaymentType.Online);
        }

        public CheckOutPaymentVM GetCheckOutPayment(long saleOrderId)
        {
            return _context.PaymentDetails.Where(p => p.OrderId == saleOrderId && p.Type == EnumPaymentType.Online)
                .Select(c=>new CheckOutPaymentVM
                {
                    PaymentStatus = c.PaymentStatus,
                    PrDatePay = c.PrDatePay,
                    SaleReferenceId = c.SaleReferenceId,
                    OrderId = c.OrderId,
                    Price = c.Price
                    
                }).FirstOrDefault();
        }

        public bool UpdatePaymentDetail(PaymentDetail paymentDetail)
        {
            paymentDetail = paymentDetail.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(paymentDetail);
            _context.SaveChanges();
            return true;
        }

        public PaymentDetail GetCreditTypePaymentDetail(long orderId)
        {
            return _context.PaymentDetails.FirstOrDefault(p => p.OrderId == orderId && p.Type == EnumPaymentType.Credit);
        }

        public PaymentDetail GetPaymentDetailByType(long orderId, EnumPaymentType type)
        {
            return _context.PaymentDetails.FirstOrDefault(p => p.OrderId == orderId && p.Type == type);
        }

        public PaymentDetail GetPaymentDetailByAuthority(string authority)
        {
            var res = _context.PaymentDetails.FirstOrDefault(p => p.Authority == authority);
            return res;
        }
    }
}
