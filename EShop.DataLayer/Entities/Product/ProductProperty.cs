using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Property;

namespace EShop.DataLayer.Entities.Product
{
   public class ProductProperty : BaseEntity
    {
         public long ProductId { get; set; }
        public long PropertyValueId { get; set; }
        [InverseProperty("ProductProperties")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [InverseProperty("ProductProperties")]
        [ForeignKey("PropertyValueId")]
        public PropertyValue PropertyValue { get; set; }
    }
}
