using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Cart
{
   public class GiftCardTransaction:BaseEntity
    {
 
        public long GiftCardId { get; set; }
        public long OrderId { get; set; }
        public int Price { get; set; }
        [ForeignKey("OrderId")]
        public Order.Order Order { get; set; }
        [ForeignKey("GiftCardId")]
        public GiftCard GiftCard { get; set; }
    }
}
