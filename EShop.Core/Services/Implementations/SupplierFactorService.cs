using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class SupplierFactorService : BaseService<SupplierFactor>, ISupplierFactorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SupplierFactorService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SupplierFactor> GetListForAdmin(EnumSupplierFactorStatus? status)
        {
            var res =  _context.SupplierFactors
                .Include(c=>c.Supplier)
                .Include(c=>c.SupplierFactorProducts)
                .ThenInclude(c=>c.WarehouseProducts)
                .OrderByDescending(c => c.Id).AsQueryable();
            if (status != null)
            {
                res = res.Where(c => c.Status == status).AsQueryable();
            }
            return res.ToList();
        }

        public List<SupplierFactor> GetListForUser()
        {
            return _context.SupplierFactors
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(SupplierFactor SupplierFactor)
        {
            try
            {
                SupplierFactor.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SupplierFactor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SupplierFactor SupplierFactor)
        {
            try
            {
                SupplierFactor.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SupplierFactor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SupplierFactor SupplierFactor)
        {
            try
            {
                SupplierFactor.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SupplierFactor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SupplierFactor FindById(long id)
        {
            return _context.SupplierFactors
                .Include(c => c.SupplierFactorProducts)
                .ThenInclude(c => c.WarehouseProducts)
                .FirstOrDefault(c=>c.Id == id);
        }
    }
}
