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
    public class SupplierFactorProductService : BaseService<SupplierFactorProduct>, ISupplierFactorProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SupplierFactorProductService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SupplierFactorProduct> GetListForAdmin()
        {
            return _context.SupplierFactorProducts
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<SupplierFactorProduct> GetListForUser()
        {
            return _context.SupplierFactorProducts
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(SupplierFactorProduct SupplierFactorProduct)
        {
            try
            {
                SupplierFactorProduct.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SupplierFactorProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SupplierFactorProduct SupplierFactorProduct)
        {
            try
            {
                SupplierFactorProduct.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SupplierFactorProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SupplierFactorProduct SupplierFactorProduct)
        {
            try
            {
                SupplierFactorProduct.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SupplierFactorProduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SupplierFactorProduct FindById(long id)
        {
            return _context.SupplierFactorProducts
                .Include(c => c.Variant).ThenInclude(c=>c.Product)
                .Include(c => c.Variant).ThenInclude(c=>c.productOption)
                .Include(c => c.Variant).ThenInclude(c=>c.Guarantee)
                 
                .Include(c => c.SupplierFactor).FirstOrDefault(c=>c.Id == id);
        }

        public List<SupplierFactorProduct> GetFactorProducts(int id)
        {
            var res = _context.SupplierFactorProducts.Where(c => c.SupplierFactorId == id)
                .Include(c => c.Variant).ThenInclude(c => c.Product)
                .Include(c => c.Variant).ThenInclude(c => c.productOption)
                .Include(c => c.Variant).ThenInclude(c => c.Guarantee)
                .Include(c => c.SupplierFactor)
                .ToList();
            return res;
        }
    }
}
