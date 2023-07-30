using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities.Order
{
    public class OrderDetail:BaseEntity
    {
         
        public int Count { get; set; }
        public int Price { get; set; }
        public int SumPrice { get; set; }
        public int Discount { get; set; }
        public int SumPriceAfterDiscount { get; set; }
        public int UnitDiscount { get; set; }
        public bool StorePlace { get; set; }
        public byte DiscountType { get; set; }
        public bool IsGiftWrapping { get; set; }

        public long VariantId { get; set; }
        public long? WarehouseProductId { get; set; }
        public long OrderId { get; set; }
         [ForeignKey("VariantId")]
        public Variety.Variant Variant { get; set; }
         [ForeignKey("OrderId")]
        public Order Order { get; set; }
         [ForeignKey(nameof(WarehouseProductId))]
        public WarehouseProduct WarehouseProduct { get; set; }
 
        public List<VariantVoteDetial> VariantVoteDetials { get; set; }
    }
}
