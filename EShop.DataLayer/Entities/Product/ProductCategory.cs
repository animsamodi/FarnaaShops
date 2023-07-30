using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product
{
   public class ProductCategory : BaseEntity
    {
     
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        [InverseProperty("ProductCategories")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [InverseProperty("ProductCategories")]
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
    }
}
