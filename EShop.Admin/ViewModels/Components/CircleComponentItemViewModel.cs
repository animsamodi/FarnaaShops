using System.ComponentModel.DataAnnotations;

namespace EShop.Admin.ViewModels.Components
{
    public class CreateCircleComponentItemViewModel
    {
        [Display(Name = "عنوان کامپوننت")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از 25 کارکتر باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "آدرس کامپوننت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
            ErrorMessage = "لطفا {0} معتبر وارد کنید")]
        public string Url { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
    public class CircleComponentItemViewModel : CreateCircleComponentItemViewModel
    {
        [Display(Name = "کد یارانه ای")]
        public long Id { get; set; }
    }
}
