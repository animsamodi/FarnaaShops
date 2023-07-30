using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Seri;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductPricePageService : IBaseService<ProductPricePage>
    {
        List<ProductPricePage> GetListForAdmin();
        List<ProductPricePage> GetListForUser();
        bool Add(ProductPricePage ProductPricePage);
        bool Update(ProductPricePage ProductPricePage);
        bool Delete(ProductPricePage ProductPricePage);
        ProductPricePage FindById(long id);
        ProductPricePage GetPriceListDetails(string categoryName, string brand, string seri);
    }
}
