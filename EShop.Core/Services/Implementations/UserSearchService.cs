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
    public class UserSearchService : BaseService<UserSearch>, IUserSearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserSearchService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<UserSearch> GetListForAdmin()
        {
            return _context.UserSearches.ToList();
        }

        public List<UserSearch> GetListForUser()
        {
            return _context.UserSearches.ToList();

        }

        public bool Add(UserSearch UserSearch)
        {
            try
            {
                UserSearch.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(UserSearch);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(UserSearch UserSearch)
        {
            try
            {
                UserSearch.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(UserSearch);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(UserSearch UserSearch)
        {
            try
            {
                UserSearch.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(UserSearch);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public UserSearch FindById(long id)
        {
            return _context.UserSearches.Find(id);
        }
    }
}
