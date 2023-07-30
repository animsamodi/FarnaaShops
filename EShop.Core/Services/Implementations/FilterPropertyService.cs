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
    public class FilterPropertyService : BaseService<FilterProperty>, IFilterPropertyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public FilterPropertyService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<FilterProperty> GetListForAdmin()
        {
            return _context.FilterProperties
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<FilterProperty> GetListForUser()
        {
            return _context.FilterProperties.Where(c=>c.IsActive).Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(FilterProperty FilterProperty)
        {
            try
            {
                FilterProperty.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(FilterProperty);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(FilterProperty FilterProperty)
        {
            try
            {
                FilterProperty.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(FilterProperty);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(FilterProperty FilterProperty)
        {
            try
            {
                FilterProperty.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(FilterProperty);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public FilterProperty FindById(long id)
        {
            return _context.FilterProperties.Find(id);
        }


    }
}
