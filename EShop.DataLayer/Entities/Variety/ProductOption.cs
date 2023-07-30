using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Variety
{
   public class ProductOption:BaseEntity
    {
       
        public byte Type { get; set; }
         [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Name { get; set; }
         [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Value { get; set; }
        [Display(Name = "کد رنگ دوم")]

        public string? SecondValue { get; set; }
        public List<Variant> Variants { get; set; }
         public List<ProductPrice> ProductPrices { get; set; }
    }
}
