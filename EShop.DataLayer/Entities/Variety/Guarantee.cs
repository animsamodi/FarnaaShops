using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Variety
{
   public class Guarantee:BaseEntity
    {
    
        [MaxLength(40)]
        public string Title { get; set; }

        public List<Variant> Variants { get; set; }
        public List<SupplierFactorProduct> SupplierFactorProducts { get; set; }

    }
}
