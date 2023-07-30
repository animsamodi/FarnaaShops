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
    public class WarehouseService : BaseService<Warehouse>, IWarehouseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public WarehouseService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<Warehouse> GetListForAdmin()
        {
            return _context.Warehouses
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<Warehouse> GetListForUser()
        {
            return _context.Warehouses
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(Warehouse Warehouse)
        {
            try
            {
                Warehouse.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(Warehouse);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Warehouse Warehouse)
        {
            try
            {
                Warehouse.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(Warehouse);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Warehouse Warehouse)
        {
            try
            {
                Warehouse.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(Warehouse);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Warehouse FindById(long id)
        {
            return _context.Warehouses.Find(id);
        }

         
    }
}
