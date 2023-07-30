using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISiteMenuService : IBaseService<SiteMenu>
    {
        List<SiteMenu> GetListForAdmin(EnumTypeSystem typeSystem);
        List<SiteMenu> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        bool Add(SiteMenu PageSeo);
        bool Update(SiteMenu PageSeo);
        bool Delete(SiteMenu PageSeo);
        SiteMenu FindById(long id);
        List<SiteMenu> GetListForDelete(EnumTypeSystem enumTypeSystem);
    }
}