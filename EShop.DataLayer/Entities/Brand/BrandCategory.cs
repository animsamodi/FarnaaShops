using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Brand
{
    public class BrandCategory:BaseEntity
    {
         public long CategoryId { get; set; }
        public long BrandId { get; set; }

        [InverseProperty("BrandCategories")]
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [InverseProperty("BrandCategories")]
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}
