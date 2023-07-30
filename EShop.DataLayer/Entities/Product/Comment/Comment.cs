using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Product.Comment
{
    public class Comment : BaseEntity
    {


        [Display(Name = "نام")]
     
        public string Name { get; set; }
        [Display(Name = "موبایل")]
       
        public string Mobile { get; set; }
        [Display(Name = "عنوان نظر")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommentTitle { get; set; }
        [Display(Name = "متن نظر")]
        [MaxLength(3000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommentText { get; set; }

 
        [Display(Name = "نقاط قوت")]
        [MaxLength(1000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Positive { get; set; }

        [Display(Name = "نقاط ضعف")]
        [MaxLength(1000, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Negative { get; set; }

        [Display(Name = "تایید شده")]
         [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumStatusComment StatusComment { get; set; }

        [Display(Name = "لایک")]
        public int CommentLike { get; set; }

        [Display(Name = "دیسلایک")]
        public int CommentDisLike { get; set; }

        public long ProductId { get; set; }
        public long? UserId { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; }
        public List<CommentLike> CommentLikes { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
