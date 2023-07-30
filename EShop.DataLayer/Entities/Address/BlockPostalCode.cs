using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Address
{
    public class BlockPostalCode:BaseEntity
    {
 

        [Display(Name = "کد")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }
        [Display(Name = "تعداد تلاش")]
        public int CountTry { get; set; }

 

    }
}