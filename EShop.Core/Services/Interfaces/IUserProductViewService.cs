using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IUserProductViewService : IBaseService<UserProductView>
    {
        List<UserProductView> GetListForAdmin();
        List<UserProductView> GetListForUser();
        bool Add(UserProductView PageSeo);
        bool Update(UserProductView PageSeo);
        bool Delete(UserProductView PageSeo);
        UserProductView FindById(long id);
        List<UserProductView> GetUserProductViewByUserId(int userid);
        List<UserProductView> GetUserProductViewByCookie(string cookie);
    }
}