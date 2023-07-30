using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.Services.Interfaces.Seo;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Core.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace EShop.Core.Services.Implementations.Seo
{
    public class RedirectService : BaseService<Redirect>, IRedirectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        //private readonly ICacheService _cacheService;
        public RedirectService(ApplicationDbContext context/*,ICacheService cacheService*/, IUserService userService)
            : base(context)
        {
            _context = context;
            _userService = userService;
            // _cacheService = cacheService;
        }
        public Tuple<int, List<Redirect>> GetListRedirect(string searchText, int pageNumber)
        {
            int skip = (pageNumber - 1) * 25;

            var query = _context.Redirects
                .Where(c => c.OldUrl.Contains(searchText)
                          || c.NewUrl.Contains(searchText)
                );

            return Tuple.Create(query.Count(), query.Skip(skip).Take(25).ToList());

        }
        public List<Redirect> GetListRedirectForAdmin()
        {
            return _context.Redirects.OrderByDescending(c => c.Id).ToList();
        }
        public Redirect FindRedirectById(long id)
        {
            return _context.Redirects.Find(id);
        }

        public bool RedirectIsExistOrNot(string oldUrl)
        {
            return _context.Redirects.Any(r => r.OldUrl == oldUrl);
        }

        public async Task<IEnumerable<Redirect>> GetAllRedirects()
        {
            //var cacheData =await _cacheService.GetAsync<IEnumerable<Redirect>>(Cache.KeysConstants.REDIRECTKEY);
            //if (cacheData==null ||( cacheData != null && cacheData.Count() < 1))
            //{
            var cacheData = await _context.Redirects.Where(i => i.IsActive && !i.IsDelete).ToListAsync();
            //await    _cacheService.SetAsync(Cache.KeysConstants.REDIRECTKEY, cacheData, TimeSpan.FromHours(1));

            //   }

            return cacheData;
        }

        public bool AddRedirect(Redirect redirect)
        {
            try
            {
                redirect.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(redirect);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditRedirect(Redirect redirect)
        {
            try
            {
                var nRedirect = FindRedirectById(redirect.Id);

                nRedirect.Id = redirect.Id;
                nRedirect.CreateDate = DateTime.Now;
                nRedirect.LastUpdateDate = DateTime.Now;
                nRedirect.IsActive = redirect.IsActive;
                nRedirect.NewUrl = redirect.NewUrl;
                nRedirect.OldUrl = redirect.OldUrl;

                nRedirect.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(nRedirect);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteRedirect(Redirect redirect)
        {
            try
            {
                redirect = redirect.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(redirect);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task ResetCache()
        {
            //await _cacheService.RemoveCacheAsync(Cache.KeysConstants.REDIRECTKEY);
            var cacheData = _context.Redirects.Where(i => i.IsActive && !i.IsDelete).ToList();
            //_cacheService.SetAsync(Cache.KeysConstants.REDIRECTKEY, cacheData);
        }
    }
}
