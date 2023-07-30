using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class ProductOtherPageService : BaseService<ProductOtherPage>, IProductOtherPageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ProductOtherPageService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ProductOtherPage> GetListForAdmin()
        {
            return _context.ProductOtherPages.ToList();
        }

        public List<ProductOtherPage> GetListForUser()
        {
            return _context.ProductOtherPages.ToList();

        }

        public bool Add(ProductOtherPage ProductOtherPage)
        {
            try
            {
                ProductOtherPage.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ProductOtherPage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ProductOtherPage ProductOtherPage)
        {
            try
            {
                ProductOtherPage.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(ProductOtherPage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ProductOtherPage ProductOtherPage)
        {
            try
            {
                ProductOtherPage.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(ProductOtherPage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductOtherPage FindById(long id)
        {
            return _context.ProductOtherPages.Find(id);
        }
    }
}
