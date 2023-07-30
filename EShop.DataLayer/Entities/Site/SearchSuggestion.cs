using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Site
{
    public class SearchSuggestion : BaseEntity
    {
 
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }
        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Order { get; set; }
        [Display(Name = "تعداد تلاش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CountTry { get; set; }
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Url { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsActive { get; set; }
    }
}