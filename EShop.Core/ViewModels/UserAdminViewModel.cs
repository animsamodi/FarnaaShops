using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
    public class UserAdminViewModel
    {
       public long Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string FullName { get; set; }
        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]


        public string Password { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [MinLength(11, ErrorMessage = "مقدار {0} نباید کمتر از{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Phone { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "نقش")]
        public string RoleTitle { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public long RoleId { get; set; }
    }

    public class UserCustomerAddressViewModel
    {


        [Display(Name = "نام و نام خانوادگی مشتری")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CustomerFullName { get; set; }
        
        [Display(Name = "شماره تلفن مشتری")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CustomerPhone { get; set; }
        
        [Display(Name = "کد ملی مشتری")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CustomerNatioalCode { get; set; }

        [Display(Name = "تاریخ ثبت نام مشتری")]
        public DateTime RegisterDate { get; set; }
   
        [Display(Name = "نام و نام خانوادگی تحویل گیرنده پیشفرض")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TransfereeFullName { get; set; }

        [Display(Name = " شماره تلفن تحویل گیرنده پیشفرض")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TransfereePhone { get; set; }

        [Display(Name = "استان تحویل گیرنده پیشفرض")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TransfereeProvince { get; set; }

        [Display(Name = " شهر تحویل گیرنده پیشفرض")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TransfereeCity { get; set; }


        [Display(Name = " آدرس پستی تحویل گیرنده پیشفرض")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string TransfereePostalAddress { get; set; }

        [Display(Name = "کد پسنی تحویل گیرنده پیشفرض")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TransfereePostalCode { get; set; }
        [Display(Name = "کد کاربر")]
        public long UserId { get; set; }
        [Display(Name = "کد آدرس")]
        public long AddressId { get; set; }

        [Display(Name = "جمع مبلغ سفارشات")]
        public long SumPrice { get; set; }

        [Display(Name = "جمع تعداد سفارشات")]
        public long CountPrice { get; set; }

        [Display(Name = "تاریخ آخرین سفارش")]
        public DateTime? DateLastOrder { get; set; }
 

    }
}