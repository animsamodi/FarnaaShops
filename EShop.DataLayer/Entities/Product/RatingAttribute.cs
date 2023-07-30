using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product.Comment;

namespace EShop.DataLayer.Entities.Product
{
    public class RatingAttribute : BaseEntity
    {
   
        [Display(Name = "نام ")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        public List<CategoryRating> CategoryRatings { get; set; }
        public List<ProductReviewRating> ProductReviewRatings { get; set; }
        public List<UserCommentRating> UserCommentRatings { get; set; }
        public List<CommentRating> CommentRatings { get; set; }
    }
}
