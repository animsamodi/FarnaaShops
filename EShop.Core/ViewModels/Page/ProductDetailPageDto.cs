using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.Core.ViewModels.Seo;

namespace EShop.Core.ViewModels.Page
{
    public class ProductDetailPageDto
    {
        public ProductDetailUserViewModel Details { get; set; }
        public List<VariantsForProductDetailViewModel> Variants { get; set; }
        public List<SellerForProductDetialViewModel> Sellers { get; set; }
        public List<ProductPropertyUserViewModel> Properties { get; set; }
        public List<ProductViewModel> RelatedProducts { get; set; }
        public ProductReviewViewModel Review { get; set; }
        public List<CommentForUserViewModel> Comments { get; set; }
        public List<ProductViewModel> SimilarProducts { get; set; }

        public string Schema { get; set; }
    }  public class CategoryPageDto
    {
        public MainCategoryPageWithFilterViewModel CategoryPageWithFilterViewModel { get; set; }
 
        public PageStructureViewModel Structure { get; set; }
        public string ReturnUrl { get; set; }
      
    }
}
