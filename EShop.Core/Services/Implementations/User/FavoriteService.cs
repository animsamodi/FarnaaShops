using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.User
{
    public class FavoriteService : BaseService<UserProductFovorites>, IFavoriteService
    {
        private ApplicationDbContext _context
            ;
        private readonly IUserService _userService;

        public FavoriteService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<FavoriteListForProfileViewModel> GetUserProductFavorites  (int userid)
        {
            var isUserColleague = _userService.IsUserColleague();

            return _context.UserProductFovoriteses.Where(u => u.UserId == userid)
                .Include(c=>c.Product)
                .ThenInclude(c=>c.Variants )
                .Include(c=>c.Product).ThenInclude(c=>c.Category)
                .Select(u => new FavoriteListForProfileViewModel {
                FavoriteId=u.Id,
                ProductTitle=u.Product.FaTitle,
                ProductCategory=u.Product.Category.EnTitle,
                EnTitle= u.Product.EnTitle,
                Productid=u.ProductId,
                ImgName=u.Product.ImgName,
                price = isUserColleague ?
                    u.Product.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? u.Product.Variants.Where(c=> c.Count > 0 && c.SellingColleauge).OrderByDescending(c=>c.PriceColleague).FirstOrDefault().PriceColleague : 0:
                    u.Product.Variants.Any(c => c.Count > 0)? u.Product.Variants.Where(c=>c.Count > 0).OrderByDescending(c=>c.SepcialPrice).FirstOrDefault().SepcialPrice : 0

            }).ToList();
        }

        public bool CheckEsxistProductFavoriteForUser(int userid, long favoriteid)
        {
            return _context.UserProductFovoriteses.Any(u=>u.UserId==userid && u.Id== favoriteid);
        }


        public bool CheckEsxistProductFavoriteForUserByProductId(int userid, int productid)
        {
            return _context.UserProductFovoriteses.Any(u => u.UserId == userid && u.ProductId == productid);
        }

        public bool RemoveUserProductFavorite(UserProductFovorites userProductFovorites)
        {
            var fav = _context.UserProductFovoriteses.Find(userProductFovorites.Id);
            fav = fav.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(fav);
                _context.SaveChanges();
                return true;
           
        }

        public long GetFavoriteId(int userid, long productid)
        {
            try
            {
                return _context.UserProductFovoriteses.Where(u => u.UserId == userid && u.ProductId == productid).AsNoTracking().FirstOrDefault().Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool AddProductFavorite(UserProductFovorites userProductFovorites)
        {
            try
            {
              userProductFovorites =  userProductFovorites.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(userProductFovorites);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
