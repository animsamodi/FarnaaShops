using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Entities.Variety;

namespace EShop.Core.Services.Interfaces
{
    public interface IColleaguePriceRangeService : IBaseService<ColleaguePriceRange>
    {
        List<ColleaguePriceRange> GetListForAdmin();
        List<ColleaguePriceRange> GetListForUser();
        bool Add(ColleaguePriceRange ColleaguePriceRange);
        bool Update(ColleaguePriceRange ColleaguePriceRange);
        bool Delete(ColleaguePriceRange ColleaguePriceRange);
        ColleaguePriceRange FindById(long id);
        ColleaguePriceRange GetDataByItems(long? berand, long? category, long? seri);
        List<ColleaguePriceRange> GetListActiveRange();
    }
}