using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Credit
{
    public class CreditDocumentType : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نوع فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeFile TypeFile { get; set; }
        [Display(Name = "توضیح")]
         public string Text { get; set; }
         [Display(Name = "آیا اجباری است ؟")]
         public bool IsRequired { get; set; }
        [Display(Name = "نوع کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumRealLegal RealLegal { get; set; }

        public List<CreditDocument> CreditDocuments { get; set; }
    }
}