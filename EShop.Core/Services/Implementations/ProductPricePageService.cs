using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Seri;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class ProductPricePageService : BaseService<ProductPricePage>, IProductPricePageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ProductPricePageService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ProductPricePage> GetListForAdmin()
        {
            return _context.ProductPricePages.Include(c => c.Category)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<ProductPricePage> GetListForUser()
        {
            return _context.ProductPricePages.Include(c => c.Category)
                .Include(c => c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(ProductPricePage ProductPricePage)
        {
            try
            {
                ProductPricePage.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ProductPricePage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ProductPricePage ProductPricePage)
        {
            try
            {
                ProductPricePage.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(ProductPricePage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ProductPricePage ProductPricePage)
        {
            try
            {
                ProductPricePage.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(ProductPricePage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductPricePage FindById(long id)
        {
            return _context.ProductPricePages.Find(id);
        }

       

        public ProductPricePage GetPriceListDetails(string categoryName, string brand, string seri)
        {
            var productPricePages = _context.ProductPricePages
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductSeri)
                .FirstOrDefault(c => c.IsActive &&
                                     (c.Category.EnTitle.Contains(categoryName) ||
                                      string.IsNullOrEmpty(categoryName)) &&
                                     (c.Brand.EnTitle.Contains(brand) || string.IsNullOrEmpty(brand)) &&
                                     (c.ProductSeri.EnTitle.Contains(seri) || string.IsNullOrEmpty(seri)));

            return productPricePages;
        }
    }
}
