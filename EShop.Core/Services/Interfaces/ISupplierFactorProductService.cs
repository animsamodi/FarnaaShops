using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;

namespace EShop.Core.Services.Interfaces
{
    public interface ISupplierFactorProductService : IBaseService<SupplierFactorProduct>
    {
        List<SupplierFactorProduct> GetListForAdmin();
        List<SupplierFactorProduct> GetListForUser();
        bool Add(SupplierFactorProduct SupplierFactorProduct);
        bool Update(SupplierFactorProduct SupplierFactorProduct);
        bool Delete(SupplierFactorProduct SupplierFactorProduct);
        SupplierFactorProduct FindById(long id);
        List<SupplierFactorProduct> GetFactorProducts(int id);
    }
}
