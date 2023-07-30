using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Product
{
    public class ProductReviewRating : BaseEntity
    {
 
        public long ProductId { get; set; }
        public long RatingAttributeId { get; set; }
        public byte Value { get; set; }
        [InverseProperty("ProductReviewRatings")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [InverseProperty("ProductReviewRatings")]
        [ForeignKey("RatingAttributeId")]
        public RatingAttribute RatingAttribute { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
