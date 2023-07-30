using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product
{
    public class RelatedProduct : BaseEntity
    {
     
        public long ProductId1 { get; set; }
        public long ProductId2 { get; set; }
        [ForeignKey("ProductId1")]
        [InverseProperty("RelatedProducts1")]

        public Product Product1 { get; set; }
        [ForeignKey("ProductId2")]
        [InverseProperty("RelatedProducts2")]

        public Product Product2 { get; set; }
 
    }
}