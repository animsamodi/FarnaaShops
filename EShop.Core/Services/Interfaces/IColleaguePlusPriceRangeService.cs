using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Variety;

namespace EShop.Core.Services.Interfaces
{
    public interface IColleaguePlusPriceRangeService : IBaseService<ColleaguePlusPriceRange>
    {
        List<ColleaguePlusPriceRange> GetListForAdmin();
        List<ColleaguePlusPriceRange> GetListForUser();
        bool Add(ColleaguePlusPriceRange ColleaguePriceRange);
        bool Update(ColleaguePlusPriceRange ColleaguePriceRange);
        bool Delete(ColleaguePlusPriceRange ColleaguePriceRange);
        ColleaguePlusPriceRange FindById(long id);
        ColleaguePlusPriceRange GetDataByItems(long? berand, long? category, long? seri);
        List<ColleaguePlusPriceRange> GetListActiveRange();
    }
}