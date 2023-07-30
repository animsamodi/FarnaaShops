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
    public class ColleaguePriceRangeService : BaseService<ColleaguePriceRange>, IColleaguePriceRangeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ColleaguePriceRangeService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ColleaguePriceRange> GetListForAdmin()
        {
            return _context.ColleaguePriceRanges
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<ColleaguePriceRange> GetListForUser()
        {
            return _context.ColleaguePriceRanges.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(ColleaguePriceRange ColleaguePriceRange)
        {
            try
            {
                ColleaguePriceRange.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ColleaguePriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ColleaguePriceRange ColleaguePriceRange)
        {
            try
            {
                ColleaguePriceRange.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(ColleaguePriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ColleaguePriceRange ColleaguePriceRange)
        {
            try
            {
                ColleaguePriceRange.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(ColleaguePriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ColleaguePriceRange FindById(long id)
        {
            return _context.ColleaguePriceRanges.Find(id);
        }

        public ColleaguePriceRange GetDataByItems(long? brand, long? category, long? seri)
        {
            var res = _context.ColleaguePriceRanges.AsQueryable();
            if (brand != null)
                res = res.Where(c => c.BrandId == brand);
            if (category != null)
                res = res.Where(c => c.CategoryId == category);
            if (seri != null)
                res = res.Where(c => c.SeriId == seri);
            return res.FirstOrDefault();


        }

        public List<ColleaguePriceRange> GetListActiveRange()
        {
            var res = _context.ColleaguePriceRanges.Where(c => c.IsActive).ToList();
            return res;
        }
    }
}