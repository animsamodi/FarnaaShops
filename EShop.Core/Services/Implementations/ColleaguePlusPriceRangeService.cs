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
    public class ColleaguePlusPriceRangeService : BaseService<ColleaguePlusPriceRange>, IColleaguePlusPriceRangeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ColleaguePlusPriceRangeService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ColleaguePlusPriceRange> GetListForAdmin()
        {
            return _context.ColleaguePlusPriceRanges
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<ColleaguePlusPriceRange> GetListForUser()
        {
            return _context.ColleaguePlusPriceRanges.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(ColleaguePlusPriceRange ColleaguePlusPriceRange)
        {
            try
            {
                ColleaguePlusPriceRange.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ColleaguePlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ColleaguePlusPriceRange ColleaguePlusPriceRange)
        {
            try
            {
                ColleaguePlusPriceRange.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(ColleaguePlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ColleaguePlusPriceRange ColleaguePlusPriceRange)
        {
            try
            {
                ColleaguePlusPriceRange.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(ColleaguePlusPriceRange);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ColleaguePlusPriceRange FindById(long id)
        {
            return _context.ColleaguePlusPriceRanges.Find(id);
        }

        public ColleaguePlusPriceRange GetDataByItems(long? brand, long? category, long? seri)
        {
            var res = _context.ColleaguePlusPriceRanges.AsQueryable();
            if (brand != null)
                res = res.Where(c => c.BrandId == brand);
            if (category != null)
                res = res.Where(c => c.CategoryId == category);
            if (seri != null)
                res = res.Where(c => c.SeriId == seri);
            return res.FirstOrDefault();


        }

        public List<ColleaguePlusPriceRange> GetListActiveRange()
        {
            var res = _context.ColleaguePlusPriceRanges.Where(c => c.IsActive).ToList();
            return res;
        }
    }
}