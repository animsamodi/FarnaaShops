using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class CreateBlogViewModel 
    {



 
        public long Id { get; set; }

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
    
        public string Image { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImgName { get; set; }

        

        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }

        [Display(Name = "تگ")]
        public string Tag { get; set; }
 
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
