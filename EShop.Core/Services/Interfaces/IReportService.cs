using System.Collections.Generic;
using System.Linq;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Interfaces
{
   public interface IReportService : IBaseService<OrderDetail>
    {
        IQueryable<ReportOrder> GetOrderReport();
    }
}
