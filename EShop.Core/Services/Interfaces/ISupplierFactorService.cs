using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISupplierFactorService : IBaseService<SupplierFactor>
    {
        List<SupplierFactor> GetListForAdmin(EnumSupplierFactorStatus? status);
        List<SupplierFactor> GetListForUser();
        bool Add(SupplierFactor SupplierFactor);
        bool Update(SupplierFactor SupplierFactor);
        bool Delete(SupplierFactor SupplierFactor);
        SupplierFactor FindById(long id);
    }
}
