using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Promotion
{
    public class Promotion:BaseEntity
    {
 
        public byte Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateProductAdd { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        public long? CmpId { get; set; }


        public long? CategoryId { get; set; }
        public long? BrandId{ get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        public List<VariantPromotion> VariantPromotions { get; set; }
    }
}
