using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Cart;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        bool UserExistOrder(int userid);
        int GetUserUseDiscountCodeCount(int userid, long discountcodeid);
        long AddOrder(Order order);
        void UpdateOrder(DataLayer.Entities.Order.Order order);
        Order GetOrderById(long orderid);
        List<UserOrder> GetListUserOrder(long userId);
        UserOrder GetOrderForPrint(long id,long userId);
        Tuple<int, List<UserOrderAdmin>> GetListUserOrderAdmin(UserOrderSearchAdmin search,bool? isColleague = null);
        Tuple<int, List<PaymentDetail>> GetListUserPaymentAdmin(string RefId = "", string PSDate = "", string PEDate = "", long? OrderId = null, int PageNumber = 1,int count=15);

        bool ConfirmOrder(long id);
        bool ConfirmOrderAnbar(long id);
        List<PaymentDetail> GetPaymentDetailsForAdmin();
        List<OrderDetailForCrmViewModel> GetOrderDetailsForCrm(long orderid);
        void SendOrderToCrm(long orderid,int crmValidationItem);
        Order GetOrderAndDetailByOrderId(long id);
        List<string> GetOrderProductsByOrderId(long id);
        List<OrderLimit> GetListOrderLimit();
        bool CreateOrderLimit(OrderLimit orderLimit);
        bool EditOrderLimit(OrderLimit orderLimit);
        bool DeleteOrderLimit( long id);
        OrderLimit GetOrderLimitById( long id);
        bool IsUserLimit(long userid, long orderId);
        void OrderChecked(long id);

        void ReturnCountToProductVariant();

        void ConverPreFactorToFactor(ConvertPreFactorToFactorViewModel model);
        List<Tuple<long,string>> ChangeOrderState(ChangeOrderStateViewModel model);
        void UploadPostCode(PostCodeViewModel model);
        void CheckOrderPaymentVerify(long orderId);
        void AddAsanPardakhtLog(AsanPardakhtLog asanPardakhtLog);
        bool EditOrder(Order data);
        UserOrder GetOrderForPrintAdmin(long id);
        Order GetOrderAndDetailsById(long id);
        bool UpdateOrderDetails(OrderDetail productInOrder);
        OrderDetail GetOrderDetailsById(long id);
    }
}
