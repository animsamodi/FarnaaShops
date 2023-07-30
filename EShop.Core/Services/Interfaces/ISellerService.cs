using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Seller;

namespace EShop.Core.Services.Interfaces
{
    public interface ISellerService : IBaseService<Seller>
    {
        List<SellerForProductDetialViewModel> GetSellresById(List<long> sellresid);
    }
}
