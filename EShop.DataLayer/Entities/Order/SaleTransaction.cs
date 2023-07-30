using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Order
{
   public class SaleTransaction:BaseEntity
    {
      
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int UnitDiscount { get; set; }
        public int SumPriceAfterDiscount { get; set; }


        public long VariantId { get; set; }
        public long OrderId { get; set; }
        [ForeignKey("VariantId")]
        public Variety.Variant Variant { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
