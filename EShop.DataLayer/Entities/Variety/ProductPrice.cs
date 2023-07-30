using EShop.DataLayer.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.DataLayer.Entities.Variety
{
    public class ProductPrice : BaseEntity
    {

        public DateTime SubmitDate { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public byte DiscountPercent { get; set; }
        public bool IsAvailable { get; set; }

        public long VariantId { get; set; }
        public long ProductOptionId { get; set; }
        [ForeignKey("VariantId")]
        public Variant Variant { get; set; }
        [ForeignKey("ProductOptionId")]
        public ProductOption ProductOption { get; set; }
    }
}
