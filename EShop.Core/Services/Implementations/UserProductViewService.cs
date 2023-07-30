using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class UserProductViewService : BaseService<UserProductView>, IUserProductViewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserProductViewService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<UserProductView> GetListForAdmin()
        {
            return _context.UserProductViews.Include(c=>c.User)
                .Include(c=>c.Product)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<UserProductView> GetListForUser()
        {
            return _context.UserProductViews
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool Add(UserProductView UserProductView)
        {
            try
            {
                UserProductView.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(UserProductView);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(UserProductView UserProductView)
        {
            try
            {
                UserProductView.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(UserProductView);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(UserProductView UserProductView)
        {
            try
            {
                UserProductView.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(UserProductView);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public UserProductView FindById(long id)
        {
            return _context.UserProductViews.Find(id);
        }

        public List<UserProductView> GetUserProductViewByUserId(int userid)
        {
            var res = _context.UserProductViews.Include(c=>c.Product).ThenInclude(c=>c.Category).Where(c => c.UserId == userid).OrderByDescending(c => c.Id).Take(10)
                .ToList();
            return res; 
        }

        public List<UserProductView> GetUserProductViewByCookie(string cookie)
        {
            var res = _context.UserProductViews.Include(c => c.Product).ThenInclude(c => c.Category).Where(c => c.Cookie == cookie).OrderByDescending(c => c.Id).Take(10)
                .ToList();
            return res;
        }
    }
}
