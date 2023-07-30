using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Banner
{
  public  class Banner:BaseEntity
    {
         
        [Display(Name ="عنوان")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشید")]
        public string Title { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

     }
}
