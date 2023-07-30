using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Order
{
    public class ShipmentService : BaseService<Shipment>, IShipmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ShipmentService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public long AddShipment(Shipment shipment)
        {
            shipment = shipment.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(shipment);
            _context.SaveChanges();
            return shipment.Id;
        }

        public long UpdateShipment(Shipment shipment)
        {
            shipment = shipment.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(shipment);
            _context.SaveChanges();
            return shipment.Id;
        }


        public void AddOrderDetailList(List<OrderDetail> orderDetails)
        { foreach (var orderDetail in orderDetails)
            {
                orderDetail.IsDelete = false;
                orderDetail.CreateDate = DateTime.Now;
                orderDetail.LastUpdateDate = DateTime.Now;
                
             }
            _context.AddRange(orderDetails);
            _context.SaveChanges();
        }
 

        public List<OrderDetail> GetOrderDetialByOrderId(int orderid)
        {
            return _context.OrderDetails.Where(c => c.OrderId == orderid).ToList();
        }

        public List<ShipmentForSubmitOrder> GetShipmentByAddress(long? cartAddressId, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var type = EnumTypeShipment.User;
            var isUserColleague = _userService.IsUserColleague();
            if (isUserColleague)
                type = EnumTypeShipment.Colleauge;
                            var quary = _context.Shipments.AsQueryable().Where(c =>c.TypeSystem == typeSystem && c.IsActive && (c.Type == EnumTypeShipment.Both || c.Type == type))
                .Select(c => new ShipmentForSubmitOrder
                {
                    CityId = c.CityId,
                    Price = c.Price,
                    PricePerAddProduct = c.PricePerAddProduct,
                    ProvinceId = c.ProvinceId,
                    ShipmentId = c.Id,
                    Title = c.Title
                });
            if (cartAddressId != null && cartAddressId != 0)
            {
                var address = _context.UserAddresses.Find(cartAddressId);
                quary = quary.Where(c => (c.ProvinceId == address.ProvinceId || c.ProvinceId == null) && (c.CityId == address.CityId || c.CityId == null));
                return quary.ToList();

            }
            else
                return new List<ShipmentForSubmitOrder>();

        }

        public Shipment GetShipmentById(long id)
        {
            return _context.Shipments.Find(id);
            
        }

        public List<Shipment> GetShipmentForAdmin()
        {
            return _context.Shipments
                .Include(c=>c.City)
                .Include(c=>c.Province)
                .OrderByDescending(c => c.Id).ToList();
        }

        public bool CreateShipment(Shipment shipment)
        {
       
            try
            {
                shipment.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(shipment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Shipment FindShipmentById(long id)
        {
            return _context.Shipments.Find(id);
        }

        public bool EditShipment(Shipment shipment)
        {
          
            try
            {
                shipment.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(shipment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<GetShipmentForSearchViewModel> GetActiveShipments()
        {
            return _context.Shipments.Where(c => c.IsActive).Select(c => new GetShipmentForSearchViewModel
            {
                Id = 0,
                Title = c.TitleCrm
            }).Distinct().ToList();
        }

        public List<ShipmentForSubmitOrder> GetShipmentColleagueByAddress(long cartAddressId)
        {
            var type = EnumTypeShipment.Colleauge;
            var quary = _context.Shipments.AsQueryable().Where(c => c.IsActive && (c.Type == EnumTypeShipment.Both || c.Type == type))
                .Select(c => new ShipmentForSubmitOrder
                {
                    CityId = c.CityId,
                    Price = c.Price,
                    PricePerAddProduct = c.PricePerAddProduct,
                    ProvinceId = c.ProvinceId,
                    ShipmentId = c.Id,
                    Title = c.Title
                });
            if (cartAddressId != null && cartAddressId != 0)
            {
                var address = _context.UserAddresses.Find(cartAddressId);
                quary = quary.Where(c => (c.ProvinceId == address.ProvinceId || c.ProvinceId == null) && (c.CityId == address.CityId || c.CityId == null));
                return quary.ToList();

            }
            else
                return new List<ShipmentForSubmitOrder>();
        }
    }
}
