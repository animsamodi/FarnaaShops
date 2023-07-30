using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditPartnerViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string FullName { get; set; }
        [Display(Name = "درصد مالکیت( سهم )")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را با عدد وارد کنید")]
        [Range(0, 100, ErrorMessage = " {0} باید بین 0 تا 100 باشد")]
        public int Darsad { get; set; }


        public long CreditId { get; set; }

 
    }
}