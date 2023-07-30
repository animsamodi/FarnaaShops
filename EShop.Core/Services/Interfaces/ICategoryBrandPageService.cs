using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ICategoryBrandPageService : IBaseService<CategoryBrandPage>
    {
        List<CategoryBrandPage> GetListForAdmin();
        List<CategoryBrandPage> GetListForUser();
        bool Add(CategoryBrandPage model);
        bool Update(CategoryBrandPage model);
        bool Delete(CategoryBrandPage model);
        CategoryBrandPage FindById(long id);
        CategoryBrandPage GetDataByCategoryAndBrand(string catEnTitle, string brand, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);

        CategoryBrandPage GetDataByCategoryAndBrand(long catId, string brand,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);


    }
}