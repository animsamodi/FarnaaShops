using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Variety;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class PlusPriceRangeService : BaseService<PlusPriceRange>, IPlusPriceRangeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PlusPriceRangeService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<PlusPriceRange> GetListForAdmin()
        {
            return _context.PlusPriceRanges
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<PlusPriceRange> GetListForUser()
        {
            return _context.PlusPriceRanges.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(PlusPriceRange PlusPriceRange)
        {
            try
            {
                PlusPriceRange.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(PlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(PlusPriceRange PlusPriceRange)
        {
            try
            {
                PlusPriceRange.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(PlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PlusPriceRange PlusPriceRange)
        {
            try
            {
                PlusPriceRange.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(PlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PlusPriceRange FindById(long id)
        {
            return _context.PlusPriceRanges.Find(id);
        }

        public PlusPriceRange GetDataByItems(long? brand, long? category, long? seri)
        {
            var res = _context.PlusPriceRanges.AsQueryable();
            if (brand != null)
                res = res.Where(c => c.BrandId == brand);
            if (category != null)
                res = res.Where(c => c.CategoryId == category);
            if (seri != null)
                res = res.Where(c => c.SeriId == seri);
            return res.FirstOrDefault();


        }

        public List<PlusPriceRange> GetListActiveRange()
        {
            var res = _context.PlusPriceRanges.Where(c => c.IsActive).ToList();
            return res;
        }
    }
}