using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Seri;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductSeriService : IBaseService<ProductSeri>
    {
        List<ProductSeri> GetListForAdmin();
        List<ProductSeri> GetListForUser();
        bool Add(ProductSeri ProductSeri);
        bool Update(ProductSeri ProductSeri);
        bool Delete(ProductSeri ProductSeri);
        ProductSeri FindById(long id);
        List<ProductSeri> GetProductSeriesByCategoryIdAndBrandId(long categoryId, long? brandId);
        List<ProductSeri> GetSeriesByCategoryBrandId(int catId, int brandId);
        ProductSeri GetSeriByFaTitle(string seriName);
        ProductSeri GetSeriByEnTitle(string seriName);
        ProductSeri GetSeriByFaTitleAndBrandId(string seriName, long? brandId);
        ProductSeri GetSeriByEnTitleAndBrandId(string seriName, long? brandId);
    }
}
