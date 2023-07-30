using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities.Property
{
    public class PropertyName : BaseEntity
    {
      
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "ترتیب")]
        public int Priority { get; set; }

        [Display(Name = "استفاده در خلاصه")]
        public bool UseSummary { get; set; }
        [Display(Name = "نمایش در کارت محصول")]
        public bool ShowInProductCard { get; set; }
        [Display(Name = "ایکن")]
        public string Icon { get; set; }
        [Display(Name = "فونت ایکن")]
        public string FontIcon { get; set; }
        [NotMapped]
        public IFormFile IconImg { get; set; }
        [NotMapped]
        public string CurrentIconName { get; set; }

        [Display(Name = "استفاده در جستجو")]
        public bool UseSearch { get; set; }
        [Display(Name = "گروه خصوصیات")]
        public long PropertyGroupId { get; set; }

        [Display(Name = "متن راهنما")]
        [MaxLength(1000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string WikiText { get; set; }
        public int Type { get; set; }

        [InverseProperty("PropertyNames")]
        [ForeignKey("PropertyGroupId")]
        public PropertyGroup PropertyGroup { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
        public List<PropertyCategory> PropertyCategories { get; set; }

    }
}
