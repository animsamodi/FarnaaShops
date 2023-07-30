using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EndPoint.Web.Utilities;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        #region constructor

        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public BrandService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        #endregion
        public Brand GetBrandByName(string brandTitle)
        {
             return _context.Brands.FirstOrDefault(b => b.EnTitle == brandTitle);
        }  

        public List<BrandForAddProductViewModel> GetBrandsForAddProduct()
        {
            return _context.Brands.Select(b => new BrandForAddProductViewModel
            {
                Id = b.Id,
                Title = b.FaTitle
            }).ToList();
        }

        public List<BranListdAdminViewModel> GetBrandListForAdmin()
        {
            return _context.Brands.Select(b => new BranListdAdminViewModel
            {
                BrandId = b.Id,
                FaTitle = b.FaTitle,
                IsShowInFirstPage = b.IsShowInFirstPage,
                Order = b.Order
            }).OrderBy(c=>c.Order).ToList();
        }

        public bool AddBrand(Brand brand)
        {
            try
            {

                brand.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(brand);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Brand GetBrandById(long id)
        {
            return _context.Brands.Find(id);
        }

        public bool EditBrand(Brand brand)
        {
            try
            {
                brand.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(brand);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Brand> GetBrandByCategoryId(long id)
        {
            return _context.BrandCategories.Where(c => c.CategoryId == id).Select(c => c.Brand).ToList();
        }
        public List<Brand> GetBrandForFilterItems(long categoryId)
        {
            return _context.BrandCategories.Where(c => c.CategoryId == categoryId).Select(c => 
            new Brand() {
                Id=c.BrandId,
                EnTitle=c.Brand.EnTitle,
                FaTitle= c.Brand.FaTitle
            }).ToList();
        }

        public List<Brand> GetListUserShowBrand()
        {
            var res = _context.Brands.Where(c => c.IsShowInFirstPage).ToList();
            return res;
        }

        public Brand GetBrandByTitle(string title)
        {
            var res = _context.Brands.ToList();
            return res.FirstOrDefault(c => c.EnTitle.ToUrlFormat() == title);

        }

        public List<Brand> GetBrandByCategoryTitle(string title)
        {
            return _context.BrandCategories.Include(c=>c.Category).Where(c => c.Category.EnTitle.ToUrlFormat() == title.ToUrlFormat()).Select(c => c.Brand).ToList();
        }

        public List<Brand> GetListBrand()
        {
            return _context.Brands.Take(10).ToList();
        }

        public bool DeleteBrand(Brand brand)
        {
            try
            {
                brand.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(brand);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public void RemoveAllBrandCategories()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [BrandCategories]");
        }
    }
}
