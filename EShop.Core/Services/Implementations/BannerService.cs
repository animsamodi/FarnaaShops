using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class BannerService : BaseService<Banner>, IBannerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public BannerService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<BannerImage> GetBannerForAdmin()
        {
            return _context.BannerImages.ToList();
        }

        public bool ChangeActiveBanner(long id)
        {

            Banner banner = _context.Find<Banner>(id);
            banner = banner.SetEditDefaultValue(_userService.GetUserId());
            banner.IsActive = banner.IsActive != true;
            _context.Update(banner);
            var res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }

        public List<BannerImageViewModel> GetListBanner(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var res = _context.BannerImages.Where(c=>c.TypeSystem == typeSystem).Select(
                c => new BannerImageViewModel
                {
                    ImageName = c.ImageName,
                    Sort = c.Sort,
                    BannerType = c.BannerType,
                    Title = c.Title,
                    Link = c.Link
                }).ToList();

            return res;
        }

        public List<BannerImageViewModel> GetListBannerOtherPage(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var res = _context.BannerImages.Where(c=> c.TypeSystem == typeSystem && (c.BannerType == EnumBannerType.Other1 || c.BannerType == EnumBannerType.Other2 || c.BannerType == EnumBannerType.Other3 || c.BannerType == EnumBannerType.Other4)).Select(
                c => new BannerImageViewModel
                {
                    ImageName = c.ImageName,
                    Sort = c.Sort,
                    BannerType = c.BannerType,
                    Title = c.Title,
                    Link = c.Link
                }).ToList();

            return res;
        }

        public BannerImageViewModel GetSearchBanner(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var res = _context.BannerImages.Where(c => c.BannerType == EnumBannerType.HeaderSearchBanner && c.TypeSystem == typeSystem).Select(
                c => new BannerImageViewModel
                {
                    ImageName = c.ImageName,
                    Sort = c.Sort,
                    BannerType = c.BannerType,
                    Title = c.Title,
                    Link = c.Link
                }).First();

            return res;
        }
    }
}
