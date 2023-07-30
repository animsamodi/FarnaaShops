using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class CategoryBrandPageService : BaseService<CategoryBrandPage>, ICategoryBrandPageService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;


        public CategoryBrandPageService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<CategoryBrandPage> GetListForAdmin()
        {
            return _context.CategoryBrandPages.Include(c => c.Brand).ToList();
        }

        public List<CategoryBrandPage> GetListForUser()
        {
            return _context.CategoryBrandPages.Include(c=>c.Brand).ToList();

        }
        public CategoryBrandPage GetDataByCategoryAndBrand(string catEnTitle, string brand, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            try
            {
                var x = _context.CategoryBrandPages
                    .Include(c => c.Brand)
                    .Include(c => c.Category).Where(a=>!a.IsDelete).ToList();
            return    x.FirstOrDefault(c =>c.TypeSystem == typeSystem && c.Brand!=null&& c.Brand.EnTitle.ToUrlFormat() == brand.ToUrlFormat()&&c.Category.EnTitle.ToUrlFormat() == catEnTitle.ToUrlFormat());
              

            }
            catch (Exception e)
            {
                return null;
            }
         

        }

        public CategoryBrandPage GetDataByCategoryAndBrand(long catId, string brand, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.CategoryBrandPages.Include(c=>c.Brand).Include(cc=>cc.Category).FirstOrDefault(c => c.CategoryId == catId && c.Brand.EnTitle.Equals(brand));
        }

        public bool Add(CategoryBrandPage model)
        {
            try
            {
                model.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(CategoryBrandPage model)
        {
            try
            {
                model.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(CategoryBrandPage model)
        {
            try
            {
                model.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CategoryBrandPage FindById(long id)
        {
            return _context.CategoryBrandPages.Find(id);
        }
    }
}
