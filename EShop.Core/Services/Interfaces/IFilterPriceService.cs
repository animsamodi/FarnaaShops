using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IFilterPriceService : IBaseService<FilterPrice>
    {
        List<FilterPrice> GetListForAdmin();
        List<FilterPrice> GetListForUser();
        bool Add(FilterPrice PageSeo);
        bool Update(FilterPrice PageSeo);
        bool Delete(FilterPrice PageSeo);
        FilterPrice FindById(long id);
    }
}