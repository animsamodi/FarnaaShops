using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IPaymentDetialService : IBaseService<PaymentDetail>
    {
        void AddRangePaymentDetial(List<PaymentDetail> paymentDetails);
        bool UpdatePaymentDetail(PaymentDetail paymentDetail);
        PaymentDetail GetOnliyTypePaymentDetail(long orderid);
        CheckOutPaymentVM GetCheckOutPayment(long saleOrderId);
        PaymentDetail GetCreditTypePaymentDetail(long orderId);
        PaymentDetail GetPaymentDetailByType(long orderId, EnumPaymentType type);
        PaymentDetail GetPaymentDetailByAuthority(string authority);
    }
}
