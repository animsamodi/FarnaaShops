using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IShipmentService : IBaseService<Shipment>
    {
        long AddShipment(Shipment shipment);
        long UpdateShipment(Shipment shipment);
        void AddOrderDetailList(List<OrderDetail> orderDetails);
        List<OrderDetail> GetOrderDetialByOrderId(int orderid);
        List<ShipmentForSubmitOrder> GetShipmentByAddress(long? cartAddressId, EnumTypeSystem typeSystem =EnumTypeSystem.Farnaa );
        Shipment GetShipmentById(long id);
        List<Shipment> GetShipmentForAdmin();
        bool CreateShipment(Shipment shipment);
        Shipment FindShipmentById(long id);
        bool EditShipment(Shipment shipment);
        List<GetShipmentForSearchViewModel> GetActiveShipments();
        List<ShipmentForSubmitOrder> GetShipmentColleagueByAddress(long cartAddressId);
    }
}
