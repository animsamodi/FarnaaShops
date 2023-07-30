using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;

namespace EShop.Core.Services.Interfaces
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        List<Supplier> GetListForAdmin();
        List<Supplier> GetListForUser();
        bool Add(Supplier Supplier);
        bool Update(Supplier Supplier);
        bool Delete(Supplier Supplier);
        Supplier FindById(long id);
    }
}
