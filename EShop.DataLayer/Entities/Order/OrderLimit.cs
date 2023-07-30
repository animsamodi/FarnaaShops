using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Order
{
    public class OrderLimit : BaseEntity
    {
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumOrderLimitType LimitType { get; set; }
        [Display(Name = "مقدار(روز , ساعت)")]
         public int? Value { get; set; }
        [Display(Name = "تعداد")]
         public int? Count { get; set; }
        [Display(Name = "مبلغ")]
         public int? Price { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CategoryId { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "تعداد تلاش")]
        public int CountTry { get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }

        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}