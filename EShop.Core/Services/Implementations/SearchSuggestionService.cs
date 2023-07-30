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
    public class SearchSuggestionService : BaseService<SearchSuggestion>, ISearchSuggestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SearchSuggestionService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<SearchSuggestion> GetListForAdmin()
        {
            return _context.SearchSuggestions.ToList();
        }

        public List<SearchSuggestion> GetListForUser()
        {
            return _context.SearchSuggestions.Where(c=>c.IsActive).ToList();

        }

        public bool Add(SearchSuggestion SearchSuggestion)
        {
            try
            {
                SearchSuggestion.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(SearchSuggestion);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(SearchSuggestion SearchSuggestion)
        {
            try
            {
                SearchSuggestion.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(SearchSuggestion);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(SearchSuggestion SearchSuggestion)
        {
            try
            {
                SearchSuggestion.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(SearchSuggestion);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SearchSuggestion FindById(long id)
        {
            return _context.SearchSuggestions.Find(id);
        }
    }
}
