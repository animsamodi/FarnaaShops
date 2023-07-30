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
    public class SiteSettingService : BaseService<SiteSetting>, ISiteSettingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SiteSettingService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SiteSetting> GetListForAdmin()
        {
            return _context.SiteSettings
                .OrderByDescending(c => c.Id).ToList();
        }

  
        public List<SiteSetting> GetListForUser()
        {
            return _context.SiteSettings
                .OrderByDescending(c => c.Id).ToList();
        }

        public IndexMainMetaViewModel GetIndexMainMetaViewModel(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteSettings.Where(c=>c.TypeSystem == typeSystem)
                .OrderByDescending(c => c.Id).Select(s=> new IndexMainMetaViewModel
                {
                    Schema = s.Schema,
                    Canonical = s.Canonical,
                    HeaderTag = s.HeaderTag,
                    MetaDescription = s.MetaDescription,
                    MetaKeywords = s.MetaKeywords,
                    MetaTitle = s.MetaTitle,
                    BaseSchema = s.BaseSchema
                } ).FirstOrDefault();
        }

        public IndexMainMetaViewModel GetBlogMainMetaViewModel()
        {
            return _context.SiteSettings
                .OrderByDescending(c => c.Id).Select(s => new IndexMainMetaViewModel
                {
                    Schema = s.BlogSchema,
                    Canonical = s.BlogCanonical,
                    HeaderTag = s.BlogHeaderTag,
                    MetaDescription = s.BlogMetaDescription,
                    MetaKeywords = s.BlogMetaKeywords,
                    MetaTitle = s.BlogMetaTitle                  ,  BaseSchema = s.BaseSchema

                }).FirstOrDefault();
        }

        public bool Add(SiteSetting SiteSetting)
        {
            try
            {
                SiteSetting.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SiteSetting);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SiteSetting SiteSetting)
        {
            try
            {
                SiteSetting.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SiteSetting);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SiteSetting SiteSetting)
        {
            try
            {
                SiteSetting.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SiteSetting);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SiteSetting FindById(long id)
        {
            return _context.SiteSettings.Find(id);
        }

        public SiteSetting FindFirst()
        {
            return _context.SiteSettings.FirstOrDefault();
        }

        public string GetSiteRobots()
        {
            return _context.SiteSettings.FirstOrDefault()?.Robots;
        }

        public TopHeaderViewModel GetTopHeader(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteSettings.Where(c=>c.TypeSystem == typeSystem).Select(c => new TopHeaderViewModel
            {
                Id = c.Id,
                ShowTopImageBannerMobile = c.ShowTopImageBannerMobile,
                ShowTopImageBannerWeb = c.ShowTopImageBannerWeb,
                TopImageBannerMobile = c.TopImageBannerMobile,
                TopImageBannerMobileTitle = c.TopImageBannerMobileTitle,
                TopImageBannerMobileUrl = c.TopImageBannerMobileUrl,
                TopImageBannerWebUrl = c.TopImageBannerWebUrl,
                TopImageBannerWebTitle = c.TopImageBannerWebTitle,
                TopImageBannerWeb = c.TopImageBannerWeb
            }).FirstOrDefault();
        }

        public SiteSetting GetSiteSeoSetting(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteSettings.FirstOrDefault(c => c.TypeSystem == typeSystem);
        }

        public SitePaymentSettingViewModel GetSitePaymentSetting(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.SiteSettings.Where(c=>c.TypeSystem == typeSystem).Select(c => new SitePaymentSettingViewModel
            {
               ShowApIpg = c.ShowApIpg,
               ShowKishIpg = c.ShowKishIpg,
               ShowMelatIpg = c.ShowMelatIpg,
               ShowZarinPalIpg = c.ShowZarinPalIpg,
               ShowMeliIpg = c.ShowMeliIpg,
               DefaultIpg = c.DefaultIpg,
            }).FirstOrDefault();
        }

        public SiteSetting GetDataByType(EnumTypeSystem typeSystem)
        {
            return _context.SiteSettings.FirstOrDefault(c=>c.TypeSystem ==typeSystem);
        }
    }
}