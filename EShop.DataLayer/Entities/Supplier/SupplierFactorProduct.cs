using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities
{
    public class SupplierFactorProduct : BaseEntity
    {
        [Display(Name = "فاکتور")]

        public long SupplierFactorId { get; set; }
        [Display(Name = "موجودی محصول")]

        public long VariantId { get; set; }
        [Display(Name = "قیمت")]
        public double Price { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; } 
        [Display(Name = "کد افق")]

        public string Code{ get; set; }



        [ForeignKey(nameof(SupplierFactorId))]
        public SupplierFactor SupplierFactor { get; set; }

        [ForeignKey(nameof(VariantId))]
        public Variant Variant{ get; set; }
 

        public List<WarehouseProduct> WarehouseProducts { get; set; }

    }
}