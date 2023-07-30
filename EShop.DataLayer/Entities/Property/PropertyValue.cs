using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product;

namespace EShop.DataLayer.Entities.Property
{
    public class PropertyValue : BaseEntity
    {
   
        [Display(Name = "مقدار")]
        [MaxLength(2000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Value { get; set; }
        public long PropertyNameId { get; set; }
        [Display(Name = "متن راهنما")]
        [MaxLength(1000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string WikiText { get; set; }
        [InverseProperty("PropertyValues")]
        [ForeignKey("PropertyNameId")]
        public PropertyName PropertyName { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
    }
}
