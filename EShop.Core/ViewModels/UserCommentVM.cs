using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
    public class UserCommentVM
    {
 
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
         public string CommentTitle { get; set; }
         public string Name { get; set; }
         public string Mobile { get; set; }
        public string productCategoryName { get; set; }
        public string CommentText { get; set; }

 
        public string Positive { get; set; }
 
        public string Negative { get; set; }
        public EnumStatusComment StatusComment { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
    public class UserCommentAdminVM: UserCommentVM
    {
 
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string UserNationalCode { get; set; }
        public string RegisterDate { get; set; }
        public string RegisterTime { get; set; }


    }
}