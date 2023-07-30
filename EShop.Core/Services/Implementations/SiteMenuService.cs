using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class SiteMenuService : BaseService<SiteMenu>, ISiteMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SiteMenuService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SiteMenu> GetListForAdmin(EnumTypeSystem typeSystem)
        {
            return _context.SiteMenus.Where(c=>c.TypeSystem == typeSystem)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<SiteMenu> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteMenus.Where(c=>c.IsActive && c.TypeSystem == typeSystem).ToList();
        }

        public bool Add(SiteMenu SiteMenu)
        {
            try
            {
                SiteMenu.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SiteMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SiteMenu SiteMenu)
        {
            try
            {
                SiteMenu.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SiteMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SiteMenu SiteMenu)
        {
            try
            {
                SiteMenu.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SiteMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SiteMenu FindById(long id)
        {
            return _context.SiteMenus.Find(id);
        }

        public List<SiteMenu> GetListForDelete(EnumTypeSystem enumTypeSystem)
        {
            var res = _context.SiteMenus.Where(c => !c.SaveForNextChange && c.Type == EnumTypeMenu.Main && c.TypeSystem == enumTypeSystem).ToList();
            return res;
        }
    }
}
