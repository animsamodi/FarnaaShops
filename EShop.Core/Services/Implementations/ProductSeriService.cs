using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Seri;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class ProductSeriService : BaseService<ProductSeri>, IProductSeriService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ProductSeriService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ProductSeri> GetListForAdmin()
        {
            return _context.ProductSeris
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<ProductSeri> GetListForUser()
        {
            return _context.ProductSeris.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(ProductSeri ProductSeri)
        {
            try
            {
                ProductSeri.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ProductSeri);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ProductSeri ProductSeri)
        {
            try
            {
                ProductSeri.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(ProductSeri);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ProductSeri ProductSeri)
        {
            try
            {
                ProductSeri.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(ProductSeri);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductSeri FindById(long id)
        {
            return _context.ProductSeris.Find(id);
        }

        public List<ProductSeri> GetProductSeriesByCategoryIdAndBrandId(long categoryId, long? brandId)
        {
            var res = _context.ProductSeris.Where(c => c.BrandId == brandId && c.CategoryId == categoryId).ToList();
            return res;
        }

        

        public List<ProductSeri> GetSeriesByCategoryBrandId(int catId, int brandId)
        {
            var res = _context.ProductSeris.Where(c => c.CategoryId == catId && c.BrandId == brandId).ToList();
            return res;
        }

        public ProductSeri GetSeriByFaTitle(string seriName)
        {
            var res = _context.ProductSeris.ToList();
            return res.FirstOrDefault(c => c.Title.ToUrlFormat() == seriName);
        }

        public ProductSeri GetSeriByEnTitle(string seriName)
        {
            var res = _context.ProductSeris.ToList();
            return res.FirstOrDefault(c => c.EnTitle.ToUrlFormat() == seriName);
        }

        public ProductSeri GetSeriByFaTitleAndBrandId(string seriName, long? brandId)
        {
            var p = _context.Brands.FirstOrDefault(r=>r.Id== brandId);
            //"سری S سامسونگ";
            string _seriName = "";
            string[] Text = seriName.Split('-');
            if(Text!=null)
            {
                _seriName = Text[0] +" "+ Text[1] +" "+ p.FaTitle;
            }
            var a = _context.ProductSeris.ToList();
            return a.FirstOrDefault(c => c.Title.ToLowerUrl() == _seriName && c.BrandId == brandId);



            //return a.FirstOrDefault(c => c.Title.ToUrlFormat() == _seriName && c.BrandId == brandId);

            //var res = _context.ProductSeris.ToList();
            //return res.FirstOrDefault(c => c.Title.ToUrlFormat() == _seriName && c.BrandId == brandId);
        }

        public ProductSeri GetSeriByEnTitleAndBrandId(string seriName, long? brandId)
        {

            var a = _context.ProductSeris.ToList();
            var b = a.FirstOrDefault(c => c.EnTitle.ToUrlFormat() == seriName && c.BrandId == brandId);


            var res = _context.ProductSeris.ToList();
            return res.FirstOrDefault(c => c.EnTitle.ToUrlFormat() == seriName && c.BrandId == brandId);
        }
    }
}
