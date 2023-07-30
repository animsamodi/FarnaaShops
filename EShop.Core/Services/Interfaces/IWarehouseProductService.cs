using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;

namespace EShop.Core.Services.Interfaces
{
    public interface IWarehouseProductService : IBaseService<WarehouseProduct>
    {
        List<WarehouseProduct> GetListForAdmin();
        List<WarehouseProduct> GetListForUser();
        bool Add(WarehouseProduct WarehouseProduct);
        bool Update(WarehouseProduct WarehouseProduct);
        bool Delete(WarehouseProduct WarehouseProduct);
        WarehouseProduct FindById(long id);
        List<WarehouseProduct> GetListByFactorProductId(int id);
        bool AddListWarehouseProduct(List<WarehouseProduct> list);
        WarehouseProduct CheckExistIMEI(string IMEI);
        int GetCountCodeRegistered(long supplierFactorProductId);
        WarehouseProduct GetProductByIMEI(string imei);
        WarehouseProduct GetFreeProductByIMEI(string imei);
        WarehouseProduct GetProductByorderDetailId(long id);
    }
}
