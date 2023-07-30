using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Seri;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.Helpers;
using EShop.Core.ViewModels.Seo;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class PageSeoService : BaseService<PageSeo>, IPageSeoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PageSeoService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<PageSeo> GetListForAdmin()
        {
            return _context.PageSeos
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<PageSeo> GetListForUser()
        {
            return _context.PageSeos.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(PageSeo PageSeo)
        {
            try
            {
                PageSeo.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(PageSeo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(PageSeo PageSeo)
        {
            try
            {
                PageSeo.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(PageSeo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PageSeo PageSeo)
        {
            try
            {
                PageSeo.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(PageSeo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PageSeo FindById(long id)
        {
            return _context.PageSeos.Find(id);
        }

        public PageSeo GetDataByItems(long? brand, long? category, long? seri)
        {
            var res = _context.PageSeos.AsQueryable();
            if (brand != null)
                res = res.Where(c => c.BrandId == brand);
            if (category != null)
                res = res.Where(c => c.CategoryId == category);
            if (seri != null)
                res = res.Where(c => c.SeriId == seri);
            return res.FirstOrDefault();


        }

        public PageStructureViewModel GetPageStructure(long? category, long? brand, long? seri,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            //var res = _context.PageSeos.AsQueryable();
            //if (brand != null )
            //    res = res.Where(c => c.BrandId == brand);
            //if (category != null)
            //    res = res.Where(c => c.CategoryId == category);
            //if (seri != null)
            //    res = res.Where(c => c.SeriId == seri);
            var res = _context.PageSeos.Where(c => c.BrandId == brand && c.CategoryId == category && c.SeriId == seri && c.TypeSystem == typeSystem).ToList();
            return res.Select(c => new PageStructureViewModel
            {
                MetaTitle = c.MetaTitle,
                PageTitle = c.Title,
                IsNoIndex = false,
                MetaDescription = c.MetaDescription,
                MetaKeywords = c.MetaKeywords,
                OgTitle = c.MetaTitle,
                Schema = SchemaHelper.WebSiteSchema + c.Schema,
                Text = c.Text.ToLazyLoadImage(),
                EnTitle = c.EnTitle,
                FAQSchema = c.FAQSchema,
                BannerMobile = c.BannerMobile,
                Banner = c.Banner,
                BannerUrl = c.BannerUrl
            }).FirstOrDefault();
        }
    }
}
