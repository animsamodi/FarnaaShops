using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IPackingService : IBaseService<Packing>
    {
        List<Packing> GetListForAdmin();
        List<Packing> GetListForUser(EnumTypeSystem typeSystem);
        bool Add(Packing entity);
        bool Update(Packing entity);
        bool Delete(Packing entity);
        Packing FindById(long id); 
    }
}