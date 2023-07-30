using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Product
{
    public class ProductReview : BaseEntity
    {
   
        [Display(Name = "خلاصه")]
        public string Summary { get; set; }

        [Display(Name = "نقد و بررسی کوتاه")]
        public string ShortReview { get; set; }

        [Display(Name = "نقد و بررسی تخصصی")]
        public string Review { get; set; }

        [Display(Name = "نقاط قوت")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Positive { get; set; }

        [Display(Name = "نقاط ضعف")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Negative { get; set; }
        public long ProductId { get; set; }
        [InverseProperty("ProductReviews")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
