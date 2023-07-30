using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IMainMenuService : IBaseService<MainMenu>
    {
        long AddParentMenu(MainMenu mainMenu);
        bool AddSubMenu(List<MainMenu> submenu);
        List<MainMenu> GetMenuListForAdmin();
        bool DeleteSubMenu(List<MainMenu> submenu);
        bool DeleteMenu(MainMenu menu);
        MainMenu GetParentMenu(long id);
        List<MainMenu> GetSubMenuForEdit(long id);
        bool EditParentMenu(MainMenu menu);
        bool UpdateSubMenu(List<MainMenu> menulist);
        List<MainMenuShowViewModel> GetMainMenu();
    }
}
