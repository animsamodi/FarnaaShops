using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities
{
    public class Blog :BaseEntity
    {
 

       

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "متن کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortText { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
        [Display(Name = "نویسنده")]
        public string Writer { get; set; }
        [Display(Name = "اسم عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Image { get; set; }
        public string OrginalImage { get; set; }



        [Display(Name = "فیلم")]
        public string Video { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int View { get; set; }
        [Display(Name = "تعداد لایک")]
        public int Like { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }

        [Display(Name = "تگ")]
        public string Tag { get; set; } 
         public string PrDate { get; set; }

        [Display(Name = "نوع")]
        public EnumTypeBlog TypeBlog { get; set; }
        [Display(Name = "متا Title")]
        public string MetaTitle { get; set; }
        [Display(Name = "متا Description")]
        public string MetaDescription { get; set; }
        [Display(Name = "متا Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Canonical")]
        public string Canonical { get; set; }
        [Display(Name = "تگ های هدر")]
        public string HeaderTag { get; set; }
        [Display(Name = "اسکیما")]

        public string Schema { get; set; }
        public string BaseSchema { get; set; }
    }
}