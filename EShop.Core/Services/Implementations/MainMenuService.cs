using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class MainMenuService : BaseService<MainMenu>, IMainMenuService
    {
        readonly ApplicationDbContext _context; private readonly IUserService _userService;

        public MainMenuService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public long AddParentMenu(MainMenu mainMenu)
        {
            mainMenu = mainMenu.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(mainMenu);
            _context.SaveChanges();
            return mainMenu.Id;
        }

        public bool AddSubMenu(List<MainMenu> submenu)
        {
            foreach (var menu in submenu)
            {
                menu.CreateDate = DateTime.Now;
                menu.LastUpdateDate = menu.CreateDate;
                menu.IsDelete = false;
            }
            _context.AddRange(submenu);
            int res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }

        public bool DeleteMenu(MainMenu menu)
        {
            bool res = true;
            List<MainMenu> sublist = _context.MainMenus.Where(m => m.ParentId == menu.Id).ToList();
            if (sublist.Count >0)
            {
                 res = DeleteSubMenu(sublist);
            }
            
            if(res)
            {
                _context.MainMenus.Attach(menu);
                _context.Entry(menu).State = EntityState.Modified;
                menu = menu.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(menu);
                int result = _context.SaveChanges();
                if (result > 0)
                    return true;
                return false;
            }
            return false;
        }

        public bool DeleteSubMenu(List<MainMenu> submenu)
        {
            foreach (var menu in submenu)
            {
                _context.MainMenus.Attach(menu);
                _context.Entry(menu).State = EntityState.Modified;
                menu.LastUpdateDate = DateTime.Now;
                menu.IsDelete = true;
            }       
            _context.UpdateRange(submenu);

            int res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }

        public MainMenu GetParentMenu(long id)
        {
            return _context.MainMenus.FirstOrDefault(m =>  m.Id == id);
        }

        public List<MainMenu> GetMenuListForAdmin()
        {
            return _context.MainMenus.ToList();
        }

        public List<MainMenu> GetSubMenuForEdit(long id)=>  _context.MainMenus.AsNoTracking().Where(m => m.ParentId == id).ToList();
        public bool EditParentMenu(MainMenu menu)
        {
            if (menu.ParentId != null)
            {
                var parents = _context.MainMenus.Where(c => c.ParentId == menu.Id && c.Id == menu.ParentId).ToList();
                if (parents.Any())
                    return false;
            }
            _context.MainMenus.Attach(menu);
            _context.Entry(menu).State = EntityState.Modified;
            menu.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(menu);
            int res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }

        public bool UpdateSubMenu(List<MainMenu> menulist)
        {
            foreach (var menu in menulist)
            {
                menu.LastUpdateDate = DateTime.Now;

            }
            _context.UpdateRange(menulist);
            int res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }
        
        public List<MainMenuShowViewModel> GetMainMenu()
        {
            List<MainMenuShowViewModel> menuShowViewModels = new List<MainMenuShowViewModel>();


            var menu = _context.MainMenus.Select(m => new MainMenuShowViewModel {
            Link=m.Link,
            MenuId=m.Id,
            MenuTitle=m.MenuTitle,
            ParentId=m.ParentId,
            Sort=m.Sort,
            Type=m.Type
            }).ToList();
            menuShowViewModels.AddRange(menu);

            return menuShowViewModels;
        }
    }
}
