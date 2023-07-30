using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Site
{
    public class UserSearch : BaseEntity
    {
 
        [Display(Name = "متن جستجو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TextSearch { get; set; }
        [Display(Name = "تعداد تلاش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CountTry { get; set; }
        public string PrDateTry{ get; set; }
        public DateTime? DateTry{ get; set; }
        public long? UserId{ get; set; }

        [ForeignKey(nameof(UserId))]
        public User.User User{ get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}