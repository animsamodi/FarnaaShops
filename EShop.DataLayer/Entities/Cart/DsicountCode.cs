using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Migrations;

namespace EShop.DataLayer.Entities.Cart
{
    public class DiscountCode : BaseEntity
    {
        [Display(Name = "عنوان ")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "کد تخفیف")]
        [MaxLength(15, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }
        [Display(Name = "تاریخ شروع")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "تعداد")]
        public int? Count { get; set; }
        [Display(Name = "حداقل مبلغ سفارش")]
        public int? MinOrderAmount { get; set; }
        [Display(Name = "حداکثر تعداد استفاده")]
        public int? MaxUseCount { get; set; }
        [Display(Name = "برای اولین سفارش ؟")]
        public bool ForFirstOrder { get; set; }
        [Display(Name = "انتشار")]
        public bool IsPubliuc { get; set; }
        [Display(Name = "دسته بندی")]
        public long? CategoryId { get; set; }
        [Display(Name = "محصول")]

        public long? ProductId { get; set; }
        [Display(Name = "موجودی")]

        public long? VariantId { get; set; }

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product Product { get; set; }
        [ForeignKey("VariantId")]
        public Variant Variant { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
        public List<UserDiscount> UserDiscounts { get; set; }

    }
}
