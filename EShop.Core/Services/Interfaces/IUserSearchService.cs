using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
    public interface IUserSearchService : IBaseService<UserSearch>
    {
        List<UserSearch> GetListForAdmin();
        List<UserSearch> GetListForUser();
        bool Add(UserSearch UserSearch);
        bool Update(UserSearch UserSearch);
        bool Delete(UserSearch UserSearch);
        UserSearch FindById(long id);
    }
}
