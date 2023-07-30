using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities.Site
{
    public class ProductOtherPage : BaseEntity
    {

        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public EnumProductOtherPage Type { get; set; }

        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Order { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public bool IsActive { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [Display(Name = "تصویر پس زمینه")]

        public string BgImage { get; set; }

        [NotMapped]
        public IFormFile BgImageImg { get; set; }
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "تعداد محصول")]

        public int Count { get; set; }
        [Display(Name = "مرتب سازی")]

        public EnumSortLayoutIndex Sort { get; set; }
        [Display(Name = "لینک")]

        public string Url { get; set; }

        [Display(Name = "تصویر کنار")]

        public string SideImage { get; set; }

        [NotMapped]
        public IFormFile SideImageImg { get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
    }
}