using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductOtherPageService : IBaseService<ProductOtherPage>
    {
        List<ProductOtherPage> GetListForAdmin();
        List<ProductOtherPage> GetListForUser();
        bool Add(ProductOtherPage ProductOtherPage);
        bool Update(ProductOtherPage ProductOtherPage);
        bool Delete(ProductOtherPage ProductOtherPage);
        ProductOtherPage FindById(long id);
    }
}
