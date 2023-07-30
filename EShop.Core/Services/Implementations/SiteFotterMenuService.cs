using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class SiteFotterMenuService : BaseService<SiteFotterMenu>, ISiteFotterMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SiteFotterMenuService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SiteFotterMenu> GetListForAdmin()
        {
            return _context.SiteFotterMenus.ToList();
        }

        public List<SiteFotterMenu> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteFotterMenus.Where( c=>c.IsActive && c.TypeSystem == typeSystem).ToList();

        }

        public bool Add(SiteFotterMenu SiteFotterMenu)
        {
            try
            {
                SiteFotterMenu.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SiteFotterMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SiteFotterMenu SiteFotterMenu)
        {
            try
            {
                SiteFotterMenu.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SiteFotterMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SiteFotterMenu SiteFotterMenu)
        {
            try
            {
                SiteFotterMenu.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SiteFotterMenu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SiteFotterMenu FindById(long id)
        {
            return _context.SiteFotterMenus.Find(id);
        }
    }
}
