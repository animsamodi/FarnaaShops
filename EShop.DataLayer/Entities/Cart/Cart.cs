using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;

namespace EShop.DataLayer.Entities.Cart
{
   public class Cart:BaseEntity
    {
         
        [MaxLength(200)]
        public string Coockie { get; set; }
        public long? UserId { get; set; }
        public int? Credit { get; set; }

        public DateTime UpdateDate { get; set; }
        public long? AddressId { get; set; }
        public long? ShipmentId { get; set; }
        public long? PackingId { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; } 
        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
        [ForeignKey(nameof(PackingId))]
        public Packing Packing { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        [ForeignKey("AddressId")]
        public Address.UserAddress Address { get; set; }
    }
}
