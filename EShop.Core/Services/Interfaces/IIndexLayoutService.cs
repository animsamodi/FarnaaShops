using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IIndexLayoutService : IBaseService<IndexLayout>
    {
        List<IndexLayout> GetListForAdmin();
        List<IndexLayoutViewModel> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        bool Add(IndexLayout indexLayout);
        bool Update(IndexLayout indexLayout);
        bool Delete(IndexLayout indexLayout);
        IndexLayout FindById(long id);
    }
}
