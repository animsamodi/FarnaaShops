using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Implementations.Order
{
    public class SaleTransactionService : BaseService<SaleTransaction>, ISaleTransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SaleTransactionService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public void AddRangeSaleTransaction(List<SaleTransaction> saleTransactions)
        {
            foreach (var saleTransaction in saleTransactions)
            {
                 saleTransaction.IsDelete = false;
                saleTransaction.CreateDate = DateTime.Now;
                saleTransaction.LastUpdateDate = DateTime.Now;
            }
             _context.AddRange(saleTransactions);
            _context.SaveChanges();
        }
    }
}
