using EShop.DataLayer.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels.Product
{
    public class ReviewAdminViewModel
    {
        public ProductReviewContentViewModel ReviewContent { get; set; }
        public List<RatingAttributeForAddReviewViewModel> RatingAttribute { get; set; }
        public List<RatingAttributeValueForAddReviewViewModel> RatingValue { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }

    public class ProductReviewContentViewModel
    {
        public long ProductReviewId { get; set; }

        [Display(Name = "خلاصه")]
        [MaxLength(2000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Summary { get; set; }

        [Display(Name = "نقد و بررسی کوتاه")]
        [MaxLength(2000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string ShortReview { get; set; }

        [Display(Name = "نقد و بررسی تخصصی")]
        public string Review { get; set; }

        [Display(Name = "نقاط قوت")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Positive { get; set; }

        [Display(Name = "نقاط ضعف")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Negative { get; set; }
    }
    public class ProductReviewContentAjaxViewModel
    {
        public long ProductReviewId { get; set; }

        [Display(Name = "خلاصه")]
        [MaxLength(2000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Summary { get; set; }

        [Display(Name = "نقد و بررسی کوتاه")]
        [MaxLength(2000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string ShortReview { get; set; }

        [Display(Name = "نقد و بررسی تخصصی")]
        public string Review { get; set; }

        [Display(Name = "نقاط قوت")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Positive { get; set; }

        [Display(Name = "نقاط ضعف")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Negative { get; set; }
    }

    public class RatingAttributeForAddReviewViewModel
    {
        public long RatingAttributeId { get; set; }
        public string Title { get; set; }
    }

    public class RatingAttributeValueForAddReviewViewModel
    {
        public long RatingAttributeId { get; set; }
        public long Id { get; set; }
        public int Value { get; set; }
    }
}
