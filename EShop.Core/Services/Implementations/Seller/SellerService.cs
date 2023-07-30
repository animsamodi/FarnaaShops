using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;

namespace EShop.Core.Services.Implementations.Seller
{
    public class SellerService : BaseService<DataLayer.Entities.Seller.Seller>, ISellerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SellerService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<SellerForProductDetialViewModel> GetSellresById(List<long> sellresid)
        {
            return _context.Sellers.Where(s => sellresid.Contains(s.Id)).Select(s => new SellerForProductDetialViewModel
            {
                Name=s.Name,
                SellerId=s.Id,
                DeliveryTime=s.DeliveryTime,
                NoReturend=s.NoReturend,
                PostingWarranty=s.PostingWarranty,
                RegisterDate=s.RegisterDate.GetSellerRegisterDate(),
                TimleySupply=s.TimleySupply,
                Vote = s.Vote
            }).ToList(); ;
        }
    }
}
