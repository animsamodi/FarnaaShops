using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class ColleaugeSettingService : BaseService<ColleaugeSetting>, IColleaugeSettingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ColleaugeSettingService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ColleaugeSetting> GetListForAdmin()
        {
            return _context.ColleaugeSettings
                .OrderByDescending(c => c.Id).ToList();
        }

  
        public List<ColleaugeSetting> GetListForUser()
        {
            return _context.ColleaugeSettings
                .OrderByDescending(c => c.Id).ToList();
        }

 
        public bool Add(ColleaugeSetting entity )
        {
            try
            {
                entity.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ColleaugeSetting entity)
        {
            try
            {
                entity.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ColleaugeSetting entity)
        {
            try
            {
                entity.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ColleaugeSetting FindById(long id)
        {
            return _context.ColleaugeSettings.Find(id);
        }

        public ColleaugeSetting FindFirst()
        {
            return _context.ColleaugeSettings.FirstOrDefault();
        }

        public TopHeaderViewModel GetTopHeader(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.ColleaugeSettings.Where(c=>c.TypeSystem == typeSystem).Select(c=> new TopHeaderViewModel
            {
                Id = c.Id,
                ShowTopImageBannerMobile = c.ShowTopImageBannerMobile,
                ShowTopImageBannerWeb   = c.ShowTopImageBannerWeb,
                TopImageBannerMobile = c.TopImageBannerMobile,
                TopImageBannerMobileTitle = c.TopImageBannerMobileTitle,
                TopImageBannerMobileUrl = c.TopImageBannerMobileUrl,
                TopImageBannerWebUrl = c.TopImageBannerWebUrl,
                TopImageBannerWebTitle = c.TopImageBannerWebTitle,
                TopImageBannerWeb = c.TopImageBannerWeb
            } ).FirstOrDefault();
        }

        public ColleaugeSetting GetEntityByType(EnumTypeSystem typeSystem)
        {
            var res = _context.ColleaugeSettings.FirstOrDefault(c => c.TypeSystem == typeSystem);
            return res;
        }
    }
}