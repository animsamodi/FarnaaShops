using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;

namespace EShop.Core.Services.Interfaces
{
    public interface IWarehouseService : IBaseService<Warehouse>
    {
        List<Warehouse> GetListForAdmin();
        List<Warehouse> GetListForUser();
        bool Add(Warehouse Warehouse);
        bool Update(Warehouse Warehouse);
        bool Delete(Warehouse Warehouse);
        Warehouse FindById(long id);
    }
}
