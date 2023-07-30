using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities
{
    public class FilterPrice:BaseEntity
    {
        

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }

        [Display(Name = "ترتیب ")]
        public int Sort{ get; set; }

        [Display(Name = "تصویر")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageImg { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }

        [Display(Name = "سری")] 
        public long? SeriId { get; set; }

        [Display(Name = "قیمت از")]

        public int MinPrice { get; set; }
        [Display(Name = "قیمت تا")]

        public int MaxPrice { get; set; }

        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
 
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [ForeignKey("SeriId")]
        public ProductSeri ProductSeri { get; set; }
    }
}