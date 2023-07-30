using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.ViewModels
{
    public class AddressViewModel
    {
        public long UserAddressId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string FullName { get; set; }

        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]   
        [RegularExpression(@"^[0-9]*", ErrorMessage = "لطفا {0} را با اعداد انگلیسی وارد کنید")]

        public string Phone { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProvinceId { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CityId { get; set; }

        public string ProvinceName { get; set; }
        public string CityName { get; set; }

        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر از{1} باشد")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string PostalAddress { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتراز{1} باشد")]

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "لطفا {0} را با اعداد انگلیسی وارد کنید")]

        public string PostalCode { get; set; }
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Lat { get; set; }

        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Lng { get; set; }

        public bool IsDefault { get; set; }
    }

    public class AddressListViewModel
    {
        public List<AddressViewModel> AddressList { get; set; }
        public AddressViewModel Address { get; set; }
    }

    public class CityViewModel
    {
        public long CityId { get; set; }
        public string CityName { get; set; }
    }

    public class ProvinceViewModel
    {
        public long ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }

    public class AddressForSubmitOrder
    {
        public long AddressId { get; set; }
        public bool IsDefault { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }

    }
}
