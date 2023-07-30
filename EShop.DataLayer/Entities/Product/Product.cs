using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.Product.FAQ;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities.Product
{
    public class Product:BaseEntity
    {
 
        [Display(Name = "عنوان فارسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "کد اختصاصی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SpecCode { get; set; }
  
        [Display(Name = "شناسه کالا")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
         public string CommodityId { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string EnTitle { get; set; }
        [Display(Name = "رم")]

        public string Ram { get; set; }
        [Display(Name = "رام")]

        public string Rom { get; set; }

 

        [Display(Name = "عکس اصلی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string ImgName { get; set; }
        public string OrginalImage { get; set; }

        public int Weight { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public long? SeriId { get; set; }
        public bool IsPublished { get; set; }
        public bool IsAvailablesoon { get; set; }

        [MaxLength(500)]
        public string OtherTitleForSearch { get; set; }

        public int? MainPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public int View { get; set; }
        public int Sell { get; set; }


        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }   
        
      
        
        [Display(Name = "متاتگ")]
        public string Meta { get; set; }
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
        public bool Have3dFile { get; set; }
        [Display(Name = "متن پاپ آپ")]

        public string PopUpContent { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public byte DiscountType { get; set; }
        [InverseProperty("Products")]
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [InverseProperty("Products")]
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [InverseProperty("Products")]
        [ForeignKey("SeriId")]
        public ProductSeri ProductSeri { get; set; }
        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        public List<ProductReviewRating> ProductReviewRatings { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
        public List<Comment.Comment> Comments { get; set; }
        public List<UserCommentRating> UserCommentRatings { get; set; }
        public List<CommentRating> CommentRatings { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<UserProductFovorites> UserProductFovorites { get; set; }
        public List<Variant> Variants { get; set; }
         public List<RelatedProduct> RelatedProducts1 { get; set; }
        public List<RelatedProduct> RelatedProducts2 { get; set; }
        public List<ProductAccessories> ProductAccessorieses1 { get; set; }
        public List<ProductAccessories> ProductAccessorieses2 { get; set; }
        public List<DiscountCode> DiscountCodes { get; set; }
        public List<UserProductView> ProductViews { get; set; }


    }
}
