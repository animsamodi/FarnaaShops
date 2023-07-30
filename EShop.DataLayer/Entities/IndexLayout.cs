using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.DataLayer.Entities
{
    public class IndexLayout : BaseEntity
    {

        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public EnumTypeLayotIndex Type { get; set; }

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
        [Display(Name = "تصویر اصلی")]

        public string MainImage { get; set; }
        [Display(Name = "تصویر پس زمینه")]

        public string BgImage { get; set; }
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "تعداد محصول")]

        public int Count { get; set; }
        [Display(Name = "مرتب سازی")]

        public EnumSortLayoutIndex Sort { get; set; }
        [Display(Name = "لینک")]

        public string Url { get; set; }
        [Display(Name = "تصویر اول")]

        public string Image1 { get; set; }
        public string OrginalImage1 { get; set; }


        [Display(Name = "لینک تصویر اول")]

        public string ImageUrl1 { get; set; }
        [Display(Name = "تصویر دوم")]

        public string Image2 { get; set; }
        public string OrginalImage2 { get; set; }

        [Display(Name = "لینک تصویر دوم")]

        public string ImageUrl2 { get; set; }
        //
        [Display(Name = "تصویر سوم")]

        public string Image3 { get; set; }
        [Display(Name = "لینک تصویر سوم")]

        public string ImageUrl3 { get; set; }
        [Display(Name = "تصویر چهارم")]

        public string Image4 { get; set; }

        [Display(Name = "لینک تصویر چهارم")]

        public string ImageUrl4 { get; set; }
        [Display(Name = "تصویر کنار")]

        public string SideImage { get; set; }

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}