using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class WarehouseProductService : BaseService<WarehouseProduct>, IWarehouseProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public WarehouseProductService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<WarehouseProduct> GetListForAdmin()
        {
            return _context.WarehouseProducts
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<WarehouseProduct> GetListForUser()
        {
            return _context.WarehouseProducts
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(WarehouseProduct WarehouseProduct)
        {
            try
            {
                WarehouseProduct.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(WarehouseProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(WarehouseProduct WarehouseProduct)
        {
            try
            {
                WarehouseProduct.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(WarehouseProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(WarehouseProduct WarehouseProduct)
        {
            try
            {
                WarehouseProduct.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(WarehouseProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public WarehouseProduct FindById(long id)
        {
            return _context.WarehouseProducts.Find(id);
        }

        public List<WarehouseProduct> GetListByFactorProductId(int id)
        {
            return _context.WarehouseProducts.Where( c=>c.SupplierFactorProductId == id).ToList();
        }

        public bool AddListWarehouseProduct(List<WarehouseProduct> list)
        {
            try
            {
                _context.AddRange(list);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public WarehouseProduct CheckExistIMEI(string IMEI)
        {
            var res = _context.WarehouseProducts.Include(c=>c.SupplierFactorProduct)
                .ThenInclude(c=>c.Variant)
                .ThenInclude(c=>c.Product)
                .Include(c=>c.DeliveryUser)
                .FirstOrDefault(c => c.IMEI == IMEI);
            return res;
        }

        public int GetCountCodeRegistered(long supplierFactorProductId)
        {
            var res = _context.WarehouseProducts.Count(c => c.SupplierFactorProductId == supplierFactorProductId);
            return res;
        }

        public WarehouseProduct GetProductByIMEI(string imei)
        {
            var res = _context.WarehouseProducts
                .Include(c=>c.SupplierFactorProduct)
                .ThenInclude(c=>c.Variant)
                .ThenInclude(c=>c.Product)
                .FirstOrDefault(c => c.IMEI == imei);
            return res;
        }

        public WarehouseProduct GetFreeProductByIMEI(string imei)
        {
            var res = _context.WarehouseProducts
                .Include(c => c.SupplierFactorProduct)
                .ThenInclude(c => c.Variant)
                .ThenInclude(c => c.Product)
                .FirstOrDefault(c => c.IMEI == imei && !c.IsUse);
                
            return res;
        }

        public WarehouseProduct GetProductByorderDetailId(long id)
        {
            var res = _context.WarehouseProducts
                .Include(c => c.SupplierFactorProduct)
                .ThenInclude(c => c.Variant)
                .ThenInclude(c => c.Product)
                .FirstOrDefault(c => c.OrderDetailId == id && c.IsUse);

            return res;
        }
    }
}
