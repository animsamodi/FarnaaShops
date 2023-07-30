using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ICategoryMainService : IBaseService<CategotyMain>
    {
        List<CategotyMain> GetListForAdmin();
        List<CategotyMain> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        CategotyMainViewModel GetCategotyMainDetail(long id);
        CategotyMainViewModel GetCategotyMainDetailGetByTitle(string title, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        public MainCategoryPageWithFilterViewModel MainCategoryWithFilter(long id, FilterDto dto,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        bool Add(CategotyMain categotyMain);
        bool Update(CategotyMain categotyMain);
        bool Delete(CategotyMain categotyMain);
        CategotyMain FindSliderById( long id);
        CategotyMain GetByTitle(string categoryName);
    }
}