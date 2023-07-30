using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.User
{
    public class User :BaseEntity
    {
    

        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Family { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Password { get; set; }

        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Phone { get; set; }

        [Display(Name = "نام کاربری")]
         public string Username { get; set; }

        [Display(Name = "شماره کارت")]
        [MaxLength(25, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CardNumber { get; set; }

        [Display(Name = "کد ملی")]

         public string NatioalCode { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "عکس")]
        public byte Avatar { get; set; }

        [Display(Name = "کد فعال سازی تلفن")]
        public int PhoneActiveCode { get; set; }

        public DateTime? PhoneActiveCodeExpDate { get; set; }

        [Display(Name = "کد فعال سازی ایمیل")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "خبرنامه")]
        public bool IsNewsLetter { get; set; }
        //
        [Display(Name = "کاربر حقوقی ؟")]
        public bool IsHoghughi { get; set; }
        [Display(Name = "کاربر اعتباری ؟")]
        public bool IsCredit { get; set; }
        [Display(Name = "مبلغ اعتبار (تومان)")]
        public long? AcceptPrice { get; set; }
        //
        public bool IsColleague { get; set; }
        public bool IsConfirmColleague { get; set; }

        public long? AddressId { get; set; }
        public long RoleId { get; set; }
        //
        public string CodeNaghsh { get; set; }
        public string FileKartMeli { get; set; }
        public string FileParvaneKasb { get; set; }
        public string FileSanad { get; set; }
        //
        [Display(Name = "نوع فروش")]

        public EnumNoeForush? NoeForush { get; set; }
        [Display(Name = "شناسه صنفی")]

        public string ShenaseSenfi { get; set; }
        //
        [Display(Name = "شماره کارت")]
        public string CartNo { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShebaNo { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string BDate { get; set; }
        //
        //
        public EnumTypeUser TypeUser { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
        public List<UserCommentRating> UserCommentRatings { get; set; }
        public List<UserProductFovorites> UserProductFovorites { get; set; }
        public List<Cart.Cart> Carts { get; set; }
        public List<GiftCard> GiftCards { get; set; }
        public List<Order.Order> Orders { get; set; }
        public List<UserDiscount> UserDiscounts { get; set; }
        public List<Credit.Credit> Credits { get; set; }
        public List<UserLegal> UserLegals { get; set; }
        public List<CreditBill> CreditBills { get; set; }

        [InverseProperty("User")]
        public List<UserAddress> UserAddresses { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("Users")]
        public UserAddress UserAddress { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public List<CooperationRequest> CooperationRequests { get; set; }
        //
        public List<WarehouseProduct> WarehouseProductsDeliveryUser { get; set; }
        public List<WarehouseProduct> WarehouseProductsClearanceUser { get; set; }
        public List<WarehouseProduct> WarehouseProductsBuyerUser { get; set; }
        public List<UserProductView> ProductViews { get; set; }
        public List<UserSearch> UserSearches { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }


    }


}