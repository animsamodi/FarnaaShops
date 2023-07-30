using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities.Product
{
    public class ProductImage : BaseEntity
    {
  
        [Display(Name = "عکس")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImgName { get; set; }
        public string OrginalImage { get; set; }

        public long ProductId { get; set; }

        [InverseProperty("ProductImages")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public long? ProductOptionId { get; set; }
        [ForeignKey("ProductOptionId")]
        public ProductOption ProductOption { get; set; }
    }
}
