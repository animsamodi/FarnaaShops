using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product.Comment
{
    public class UserCommentRating : BaseEntity
    {
      
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public long RatingAttributeId { get; set; }
        [Display(Name = "مقدار")]
        public byte Value { get; set; }
        [ForeignKey("UserId")]

        public User.User User { get; set; }  
        
        [ForeignKey("ProductId")]

        public Product Product { get; set; }
        [ForeignKey("RatingAttributeId")]

        public RatingAttribute RatingAttribute { get; set; }
    }
}
