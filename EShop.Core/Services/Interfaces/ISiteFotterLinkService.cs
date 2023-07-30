using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISiteFotterLinkService : IBaseService<SiteFotterLink>
    {
        List<SiteFotterLink> GetListForAdmin();
        List<SiteFotterLink> GetListForUser();
        bool Add(SiteFotterLink SiteFotterLink);
        bool Update(SiteFotterLink SiteFotterLink);
        bool Delete(SiteFotterLink SiteFotterLink);
        SiteFotterLink FindById(long id);
        List<SiteFotterLink> GetSiteListFooterSocial(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<SiteFotterLink> GetSiteListFooterLicense(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
    }
}
