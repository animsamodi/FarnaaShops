using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.User;

namespace EShop.Core.Services.Interfaces
{
   public interface IFavoriteService : IBaseService<UserProductFovorites>
    {
        List<FavoriteListForProfileViewModel> GetUserProductFavorites(int userid);
        bool CheckEsxistProductFavoriteForUser(int userid, long favoriteid);
        bool RemoveUserProductFavorite(UserProductFovorites userProductFovorites);
        bool AddProductFavorite(UserProductFovorites userProductFovorites);
        long GetFavoriteId(int userid, long productid);
        bool CheckEsxistProductFavoriteForUserByProductId(int userid, int productid);
    }
}
