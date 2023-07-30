using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Implementations
{
    public class StaticPageService : BaseService<StaticPage>, IStaticPageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public StaticPageService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<StaticPage> GetListStatic()
        {
            return _context.StaticPages.Where(c=>c.IsActive).ToList();
        }

        public bool EditStaticPage(StaticPage staticPage)
        {
            try
            {
                staticPage.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(staticPage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public StaticPage GetStaticPageById(long id)
        {
            return _context.StaticPages.Find(id);
        }

        public List<StaticPage> GetSiteFooterMenu()
        {
            return _context.StaticPages.Where(c => c.IsActive).ToList();
        }

        public List<StaticPage> GetListStaticForAdmin()
        {
            return _context.StaticPages.ToList();
        }

        public StaticPage GetStaticPageByTitle(string title)
        {
            return _context.StaticPages.Where(t=>t.EnTitle.ToLower().Replace(" ","-")==title).FirstOrDefault();

        }
    }
}
