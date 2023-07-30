using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISiteSettingService : IBaseService<SiteSetting>
    {
        List<SiteSetting> GetListForAdmin();
        List<SiteSetting> GetListForUser();
        IndexMainMetaViewModel GetIndexMainMetaViewModel(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        IndexMainMetaViewModel GetBlogMainMetaViewModel();
        bool Add(SiteSetting indexLayout);
        bool Update(SiteSetting indexLayout);
        bool Delete(SiteSetting indexLayout);
        SiteSetting FindById(long id);
        SiteSetting FindFirst();
        string GetSiteRobots();
        TopHeaderViewModel GetTopHeader(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        SiteSetting GetSiteSeoSetting(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        SitePaymentSettingViewModel GetSitePaymentSetting(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        SiteSetting GetDataByType(EnumTypeSystem typeSystem);
    }
}