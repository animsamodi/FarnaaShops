using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Seo;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IPageSeoService : IBaseService<PageSeo>
    {
        List<PageSeo> GetListForAdmin();
        List<PageSeo> GetListForUser();
        bool Add(PageSeo PageSeo);
        bool Update(PageSeo PageSeo);
        bool Delete(PageSeo PageSeo);
        PageSeo FindById(long id);
        PageSeo GetDataByItems(long? brand, long? category, long? seri);
        PageStructureViewModel GetPageStructure(long? category, long? brand, long? seri, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
    }
}
