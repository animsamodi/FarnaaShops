using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
   public interface IStaticPageService : IBaseService<StaticPage>
    {

       List<StaticPage> GetListStatic();
       bool EditStaticPage(StaticPage staticPage);

       StaticPage GetStaticPageById(long id);
        StaticPage GetStaticPageByTitle(string title);

        List<StaticPage> GetSiteFooterMenu();
        List<StaticPage> GetListStaticForAdmin();
    }
}
