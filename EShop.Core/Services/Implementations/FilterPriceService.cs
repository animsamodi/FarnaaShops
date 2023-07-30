using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class FilterPriceService : BaseService<FilterPrice>, IFilterPriceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public FilterPriceService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<FilterPrice> GetListForAdmin()
        {
            return _context.FilterPrices
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<FilterPrice> GetListForUser()
        {
            return _context.FilterPrices.Where(c=>c.IsActive).Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(FilterPrice FilterPrice)
        {
            try
            {
                FilterPrice.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(FilterPrice);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(FilterPrice FilterPrice)
        {
            try
            {
                FilterPrice.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(FilterPrice);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(FilterPrice FilterPrice)
        {
            try
            {
                FilterPrice.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(FilterPrice);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public FilterPrice FindById(long id)
        {
            return _context.FilterPrices.Find(id);
        }


    }
}
