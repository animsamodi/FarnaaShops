using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditUserListViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        [Display(Name = "کد رهگیری")]
        public string TrakingCode { get; set; }
        [Display(Name = "نوع درخواست")]

        public EnumRealLegal RealLegal { get; set; }
        [Display(Name = "کد ربر")]

        public long UserId { get; set; }

        [Display(Name = "پیغام مدیریت")]
        public string AdminMessage { get; set; }
        [Display(Name = "نام کاربر")]
        public string NameUser { get; set; }
        [Display(Name = "تلفن کاربر")]
        public string UserPhone { get; set; }
        [Display(Name = "کد ملی کاربر")]
        public string UserCodeMeli { get; set; }

        [Display(Name = "پیغام کاربر")]
        public string UserMessage { get; set; }
        [Display(Name = "وضعیت")]
        public EnumCreditStatus CreditStatus { get; set; }
        [Display(Name = "مبلغ تایید شده")]
        public int AcceptPrice { get; set; }
        [Display(Name = "تاریخ ثبت")]

        public string CreateDate { get; set; }
        [Display(Name = "تاریخ آخرین تغییر")]

        public string LastUpdateDate { get; set; }
        [Display(Name = "تاریخ اعتبار")]

        public string CreditExpDate { get; set; }


    }
}