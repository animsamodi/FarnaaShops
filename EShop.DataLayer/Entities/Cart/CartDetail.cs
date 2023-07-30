using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Cart
{
   public class CartDetail:BaseEntity
    {
        
        public int Count { get; set; }
        public int Price { get; set; }
        public long CartId { get; set; }
        public bool IsActiveCart { get; set; }

        public long VariantId { get; set; }
        [ForeignKey("VariantId")]

        public Variety.Variant Variant { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
    }
}
