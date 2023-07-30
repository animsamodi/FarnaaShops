using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISiteFotterMenuService : IBaseService<SiteFotterMenu>
    {
        List<SiteFotterMenu> GetListForAdmin();
        List<SiteFotterMenu> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        bool Add(SiteFotterMenu SiteFotterMenu);
        bool Update(SiteFotterMenu SiteFotterMenu);
        bool Delete(SiteFotterMenu SiteFotterMenu);
        SiteFotterMenu FindById(long id);
     }
}
