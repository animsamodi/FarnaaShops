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
    public class SiteFotterLinkService : BaseService<SiteFotterLink>, ISiteFotterLinkService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SiteFotterLinkService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SiteFotterLink> GetListForAdmin()
        {
            return _context.SiteFotterLinks.ToList();
        }

        public List<SiteFotterLink> GetListForUser()
        {
            return _context.SiteFotterLinks.ToList();

        }

        public bool Add(SiteFotterLink SiteFotterLink)
        {
            try
            {
                SiteFotterLink.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SiteFotterLink);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SiteFotterLink SiteFotterLink)
        {
            try
            {
                SiteFotterLink.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SiteFotterLink);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SiteFotterLink SiteFotterLink)
        {
            try
            {
                SiteFotterLink.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SiteFotterLink);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SiteFotterLink FindById(long id)
        {
            return _context.SiteFotterLinks.Find(id);
        }
        public List<SiteFotterLink> GetSiteListFooterSocial(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var res = _context.SiteFotterLinks.Where(c => c.IsActive && c.Type == DataLayer.Enum.EnumTypeFotterLink.Social && c.TypeSystem == typeSystem).ToList();
            return res;
        }
        public List<SiteFotterLink> GetSiteListFooterLicense(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var res = _context.SiteFotterLinks.Where(c => c.IsActive && c.Type != DataLayer.Enum.EnumTypeFotterLink.Social && c.TypeSystem == typeSystem).ToList();
            return res;
        }
    }
}
