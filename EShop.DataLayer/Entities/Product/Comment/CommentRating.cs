using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product.Comment
{
    public class CommentRating : BaseEntity
    {
      
        public long ProductId { get; set; }
        public long RatingAttributeId { get; set; }
        [Display(Name = "مقدار")]
        public byte Value { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }
        [ForeignKey("RatingAttributeId")]

        public RatingAttribute RatingAttribute { get; set; }
    }
}
