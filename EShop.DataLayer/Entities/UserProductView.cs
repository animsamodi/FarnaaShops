using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities
{
    public class UserProductView : BaseEntity
    {
        

     

        [Display(Name = "کاربر")]

        public long? UserId { get; set; }
        [Display(Name = "کوکی")]

        public string Cookie { get; set; }
        [Display(Name = "محصول")]

        public long ProductId { get; set; }
        [Display(Name = "تعداد مشاهده")]

        public int CountView { get; set; }

      
       
        [Display(Name = "تاریخ آخرین بازدید")]
        public DateTime Date { get; set; }
        [Display(Name = "تاریخ آخرین بازدید")]

        public string PrDateTime{ get; set; }
 
        [ForeignKey(nameof(UserId))]
        public User.User User{ get; set; } 
        [ForeignKey(nameof(ProductId))]
        public Product.Product Product{ get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}