using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
   public class LoginColleagueViewModel
    {


        [Display(Name = "نام کاربری")]
        [MaxLength(12, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر از{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
         public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string Password { get; set; }

        //[Range(typeof(bool),"true","true",ErrorMessage ="باید با قوانین و مقررات موافقت کنید")]
        public bool Terms { get; set; }
    }
       public class RegisterUserViewModel
    {


        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [RegularExpression(@"^\(?(09)\)?([0-9]{9})$", ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        public string Phone { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Range(typeof(bool),"true","true",ErrorMessage ="باید با قوانین و مقررات موافقت کنید")]
        public bool Terms { get; set; }
        public EnumRealLegal Type { get; set; }
    }

    public class ActiveEmailViewModel
    {
        public long UserId { get; set; }
        public string ActiveCode { get; set; }
    }

    public class FogotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [EmailAddress(ErrorMessage = "لطفا {0} را به درستی وارد کنید")]
        public string Email { get; set; }
    }

    public class ResetPaswordViewModel
    {
        public int UserId { get; set; }
        public string ActiveCode { get; set; }
        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="تکرار کلمه عبور همخوانی ندارد")]
        public string RePassword { get; set; }

    }

    public class ActivePhoneViewModel
    {
        public long userid { get; set; }
        public string Phone { get; set; }

        [Display(Name = "کد")]
        [MaxLength(5, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string ActiveCode { get; set; }
 public string[] ActiveCodeArr { get; set; }
        public EnumRealLegal Type { get; set; }

    }

    public class LoginViewModel
    {
        [Display(Name = " شماره تلفن")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [RegularExpression("^[0-9]{11,11}$", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
     
        public string Email_Phone { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }

    public class ChangePasswordViewModel
    {

        [Display(Name = "رمز عبور قبلی")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Display(Name = "رمز عبور جدید")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور همخوانی ندارد")]
        public string RePassword { get; set; }
    }

    public class EditRoleViewModel
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public List<long> SelctedPermissons { get; set; }
        public List<Permisson> Permissons { get; set; }
    }
}
