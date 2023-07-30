using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EShop.Core.Services.Implementations
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SupplierService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<Supplier> GetListForAdmin()
        {
            return _context.Suppliers
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<Supplier> GetListForUser()
        {
            return _context.Suppliers
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(Supplier Supplier)
        {
            try
            {
                Supplier.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(Supplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Supplier Supplier)
        {
            try
            {
                Supplier.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(Supplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Supplier Supplier)
        {
            try
            {
                Supplier.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(Supplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Supplier FindById(long id)
        {
            return _context.Suppliers.Find(id);
        }
    }
}
