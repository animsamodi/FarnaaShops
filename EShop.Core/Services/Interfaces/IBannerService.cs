using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IBannerService : IBaseService<Banner>
    {
        List<BannerImage> GetBannerForAdmin();
        bool ChangeActiveBanner(long id);
        List<BannerImageViewModel> GetListBanner(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<BannerImageViewModel> GetListBannerOtherPage(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        BannerImageViewModel GetSearchBanner(EnumTypeSystem enumTypeSystem = EnumTypeSystem.Farnaa);
    }
}
