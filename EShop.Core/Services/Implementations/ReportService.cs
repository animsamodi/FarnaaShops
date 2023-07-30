using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Implementations
{
    public class ReportService : BaseService<OrderDetail>, IReportService
    {
 
        private ApplicationDbContext _context; private readonly IUserService _userService;


        public ReportService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public IQueryable<ReportOrder> GetOrderReport()
        {
            var query = _context.OrderDetails
                .Include(c=>c.Order)
                .ThenInclude(c=>c.PaymentDetails)
                .Include(c=>c.Variant)
                .ThenInclude(c=>c.Product)
                .Where(c=>  c.Order.OrderStatus != null && c.Order.OrderStatus != EnumOrderStatus.NotPay )
                .Select(c=>new ReportOrder
                {
                    Id =  c.OrderId,
                    AmountPayable = c.Order.AmountPayable / 10,
                    ClientAddress = c.Order.ClientAddress,
                    ClientName = c.Order.ClientName,
                    ClientNatioalCode = c.Order.ClientNatioalCode,
                    ClientPostalCode = c.Order.ClientPostalCode,
                    ClientTel = c.Order.ClientTel,
                    Color = c.Variant.productOption.Name,
                    Count = c.Count,
                    CreateDate = c.Order.CreateDate,
                    FaTitle = c.Variant.Product.FaTitle,
                    Guarantee = c.Variant.Guarantee.Title,
                    PaymentMethod = "",
                    Price = c.Price / 10,
                    RecipientAddress = c.Order.RecipientAddress,
                    RecipientName = c.Order.RecipientName,
                    RecipientPostalCode = c.Order.RecipientPostalCode,
                    RecipientTel = c.Order.RecipientTel,
                    SaleReferenceId = c.Order.PaymentDetails.FirstOrDefault().SaleReferenceId,
                    ShipmentPrice = c.Order.ShipmentPrice /10,
                    ShipmentTitle = c.Order.ShipmentTitle,
                    SumAmount = c.Order.SumAmount /10,
                    SumPrice = c.SumPriceAfterDiscount /10,
                    PrDate = c.CreateDate.GetShamsiDate(),
                    PrDateDisplay = c.CreateDate.GetMonthPersian()
                });
            return query;
        }
    }
}
