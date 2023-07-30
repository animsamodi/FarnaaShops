using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Core.ViewModels.Product
{
    public class ProductListViewModel
    {
        [Display(Name = "کد محصول")]
        public long Id { get; set; }

        [Display(Name = "کد اختصاصی")] 
        public string SpecCode { get; set; }
        [Display(Name = "شناسه کالا")]
         public string CommodityId { get; set; }
        [Display(Name = "عنوان")]
        public string FaTitle { get; set; }
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsExist { get; set; }
        [Display(Name = "قیمت پیش فرض")]
        public string DefaultPrice { get; set; }
        [Display(Name = "تعداد کل موجودی")]
        public int TolatCount { get; set; }
        [Display(Name = "برند")]
        public string BrnadTitle { get; set; }
        [Display(Name = "دسته بندی")]
        public string CategoryTitle { get; set; }
        [Display(Name = "به زودی موجود میشود؟")]
        public bool IsAvailablesoon { get; set; }
        public long CategoryId{ get; set; }
        public long BrnadId { get; set; }
    } public class ProductListDiscountViewModel
    {
        [Display(Name = "کد محصول")]
        public long Id { get; set; }
         
        [Display(Name = "عنوان")]
        public string FaTitle { get; set; }
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [Display(Name = "قیمت")]
        public string DefaultPrice { get; set; }
        [Display(Name = "قیمت با تخفیف")]
        public string DiscountPrice { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }
        [Display(Name = "برند")]
        public string BrnadTitle { get; set; }
        [Display(Name = "دسته بندی")]
        public string CategoryTitle { get; set; }
        [Display(Name = "رنگ")]
        public string ColorTitle { get; set; }
        [Display(Name = "گارانتی")]
        public string GarantyTitle { get; set; }
        public long CategoryId{ get; set; }
        public long BrnadId { get; set; }
    }

    public class AddProductViewModel
    {
 
        public long? Id { get; set; }
        [Display(Name = "عنوان فارسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
       // [RegularExpression(@"^[A-Za-z0-9\u0600-\u06FF\s]+$",
       //     ErrorMessage = "نام محصول نباید شامل کارکتر های غیر مجاز باشد")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [RegularExpression(@"^[A-Za-z0-9\u0600-\u06FF\s.,]+$",
            ErrorMessage = "نام محصول نباید شامل کارکتر های غیر مجاز باشد")]
        public string EnTitle { get; set; }

        [Display(Name = "کد اختصاصی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SpecCode { get; set; }
        [Display(Name = "شناسه کالا")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CommodityId { get; set; }
        public string Img { get; set; }
        [Display(Name = "تصویر اصلی")]
        public IFormFile ImgName { get; set; }

        [Display(Name = "دسته بندی")]
        public long CategoryID { get; set; }
        [Display(Name = " برند")]
        public long BrandID { get; set; }
        [Display(Name = " سری")]

        public long? SeriId { get; set; }


        [Display(Name = " انتشار")]
        public bool IsPublished { get; set; }

        [Display(Name = "رم")]

        public string Ram { get; set; }
        [Display(Name = "رام")]

        public string Rom { get; set; }
        [Display(Name = "عنوان های دیگر")]
        public string KeyWord { get; set; }
        public List<SelectListItem> drpColors { get; set; }
        [Display(Name = " رنگ بندی")]

        public long[] ColorsIds { get; set; }
        public List<SelectListItem> drpGuarantees { get; set; }
        [Display(Name = "گارانتی")]

        public long[] GuaranteesIds { get; set; }
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
        [Display(Name = "نمایش پاپ آپ")]

        public bool IsShowPopUp { get; set; }
        [Display(Name = "متن پاپ آپ")] public string PopUpContent { get; set; }

        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }

        [Display(Name = "به زودی موجود میشود؟")]
        public bool IsAvailablesoon { get; set; }

    }

}
