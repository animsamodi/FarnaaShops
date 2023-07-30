using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Address
{
   public class UserAddress : BaseEntity
    {

        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Family { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProvinceId { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CityId { get; set; }


        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string PostalAddress { get; set; }

        [Display(Name = "کد پسنی")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostalCode { get; set; }

        
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Lat { get; set; }

        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Lng { get; set; }

        public long UserId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsClientAddress { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }
        public List<User.User> Users { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
               [ForeignKey("ProvinceId")]
 public Province Province { get; set; }

        public List<Cart.Cart> Carts { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
