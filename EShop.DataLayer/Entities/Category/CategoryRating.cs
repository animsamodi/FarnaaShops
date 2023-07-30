using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product;

namespace EShop.DataLayer.Entities.Category
{
    public class CategoryRating:BaseEntity
    {
       
        public long CategoryId { get; set; }
        public long RatingAttributeId { get; set; }

        [InverseProperty("CategoryRatings")]
        [ForeignKey("CategoryId")]
        public EShop.DataLayer.Entities.Category.Category Category { get; set; }
        [InverseProperty("CategoryRatings")]
        [ForeignKey("RatingAttributeId")]
        public RatingAttribute RatingAttribute { get; set; }
    }
}
