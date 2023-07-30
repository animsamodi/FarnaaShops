using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Interfaces
{
   public interface ISaleTransactionService : IBaseService<SaleTransaction>
    {
        void AddRangeSaleTransaction(List<SaleTransaction> saleTransactions);
    }
}
