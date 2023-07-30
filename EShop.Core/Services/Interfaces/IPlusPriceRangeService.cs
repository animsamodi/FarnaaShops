using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Variety;

namespace EShop.Core.Services.Interfaces
{
    public interface IPlusPriceRangeService : IBaseService<PlusPriceRange>
    {
        List<PlusPriceRange> GetListForAdmin();
        List<PlusPriceRange> GetListForUser();
        bool Add(PlusPriceRange ColleaguePriceRange);
        bool Update(PlusPriceRange ColleaguePriceRange);
        bool Delete(PlusPriceRange ColleaguePriceRange);
        PlusPriceRange FindById(long id);
        PlusPriceRange GetDataByItems(long? berand, long? category, long? seri);
        List<PlusPriceRange> GetListActiveRange();
    }
}