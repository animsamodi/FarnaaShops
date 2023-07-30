using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class BranListdAdminViewModel
    {
        [Display(Name = "شماره")]
        public long BrandId { get; set; }

        [Display(Name = "عنوان فارسی")]
        public string FaTitle { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]

        public bool IsShowInFirstPage { get; set; }
        [Display(Name = "ترتیب")]
        public int Order { get; set; }
    }

    public class CreateBrandViewModel
    {
        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
[Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }
        [Display(Name = "ترتیب")]
        public int Order { get; set; }
    }

    public class EditBrandViewModel
    {
        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
        
        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]

        public bool IsShowInFirstPage { get; set; }
        [Display(Name = "ترتیب")]
        public int Order { get; set; }
    }
}
