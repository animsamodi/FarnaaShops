using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }
        [Display(Name = "عکس")]

        public IFormFile Image { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
        public List<long> ParentList { get; set; }
        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }
    } 
    public class GetCategoryForTree
    {     
        public long Id { get; set; }

        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }
        [Display(Name = "عکس")]

        public IFormFile Image { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
        [Display(Name = "والد ")]
        public long? ParentId { get; set; }

        [Display(Name = "اسکیما FAQ ")]
        public string FAQSchema { get; set; }

        [Display(Name = "عنوان متا")]
        public string MetaTitle { get; set; }
    }

    public class GetCategoryForAddViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
    public class GetShipmentForSearchViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }

    public class EditCategoryViewModel
    {
        public long Id { get; set; }
        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }
        public string CurrentImage { get; set; }
        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }
        public List<long> ParentList { get; set; }    
        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
        [Display(Name = "اسکیما FAQ")] public string FAQSchema { get; set; }
    }


    public class SearchCategoryViewModel
    {
        public long CategoryId { get; set; }
        public string CategoryEnTitle { get; set; }
        public string CategoryFaTitle { get; set; }

        public string ParentCategoryEnTitle { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public bool haveChild { get; set; }
    }
}
