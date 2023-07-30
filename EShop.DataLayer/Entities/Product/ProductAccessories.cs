using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product
{
    public class ProductAccessories : BaseEntity
    {
     
        public long ProductId1 { get; set; }
        public long ProductId2 { get; set; }
        [ForeignKey("ProductId1")]
        [InverseProperty("ProductAccessorieses1")]

        public Product Product1 { get; set; }
        [ForeignKey("ProductId2")]
        [InverseProperty("ProductAccessorieses2")]

        public Product Product2 { get; set; }
 
    }
}