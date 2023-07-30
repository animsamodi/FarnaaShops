using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities.Promotion
{
    public class VariantPromotion:BaseEntity
    {
 
        public int Price { get; set; }
        public byte Percent { get; set; }
        public int Count { get; set; }
        public int? MaxOrderCount { get; set; }
        public int ReminaingCount { get; set; }
        public byte Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        
        public long VariantId { get; set; }
        public long PromotionId { get; set; }
        [ForeignKey("VariantId")]
        public Variant Variant { get; set; }
        [ForeignKey("PromotionId")]
        public Promotion Promotion { get; set; }
    }
}
