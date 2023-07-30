using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EndPoint.Web.Utilities;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Migrations;
using Infrastructure.CacheHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Office.Interop.Excel;
using Nest;
using EShop.DataLayer.Entities.Product;
using EShop.Core.Services.Implementations.Product;
using EShop.Core.ViewModels.Page;
using Microsoft.CodeAnalysis;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        private readonly IVariantService _variantService;
        private readonly ISellerService _sellerService;
        private IUserProductViewService _userProductViewService; ICategoryService _categoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
     

        public ProductController(IProductService productService, ICommentService commentService, IVariantService variantService, ISellerService sellerService, IUserProductViewService userProductViewService, ICategoryService categoryService, IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _commentService = commentService;
            _variantService = variantService;
            _sellerService = sellerService;
            _userProductViewService = userProductViewService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
          
        }




        [Route("Product/{id}")]
        [Route("Product/{id}/{name}")]

        public IActionResult Index(int id, string name)
        {
            var productName = _productService.GetProductNameWithId(id);
            if (productName == null)
                return NotFound();
            return RedirectToActionPermanent(nameof(Index), new { id = id, name = productName.EnTitle.ToUrlFormat().Trim('-').Trim(), category = productName.Category.EnTitle });
        }

        [Route("Product/{id}/{category}/{name}")]
        public IActionResult Index(int id, string name, string category)
        {

            var productPageData = new ProductDetailPageDto();
     
            if (category.IsPersianWord())
            {
                var catFa = _categoryService.GetCategoryByFaTitle(category);
                if (catFa != null)
                    return RedirectToActionPermanent(nameof(Index), new { id = id, name = name.Trim('-').Trim(), category = catFa.EnTitle });
            }

            if (name.StartsWith("-") || name.EndsWith("-"))
            {
                name = name.Trim('-').Trim();
                return RedirectToActionPermanent(nameof(Index), new { id, name, category });

            }


            long userId = 0;
       
                ProductDetailUserViewModel product = _productService.GetProductDetailUser(id, userId);
                var variants = _variantService.GetVariantsByProductId(id);
                var seller = _sellerService.GetSellresById(variants.GroupBy(v => v.SellerId).Select(v => v.Key).ToList());
                product.CategoryEnName = category;

                if (product == null)
                    return NotFound();
                string schema = SchemaHelper.GenrateproductPageSchema(product, category);

                productPageData = new ProductDetailPageDto
                {
                    Sellers = new List<SellerForProductDetialViewModel>(),
                    Details = new ProductDetailUserViewModel(),
                    Variants = new List<VariantsForProductDetailViewModel>(),
                    Comments = new List<CommentForUserViewModel>(),
                    Properties = new List<ProductPropertyUserViewModel>(),
                    RelatedProducts = new List<ProductViewModel>(),
                    Review = new ProductReviewViewModel(),
                    SimilarProducts = new List<ProductViewModel>()
                };
                productPageData.Details = product;
                productPageData.Sellers = seller;
                productPageData.Variants = variants;
                productPageData.Schema = schema;
                //

                var comments = new List<CommentForUserViewModel>();
                var properties = new List<ProductPropertyUserViewModel>();
                var relatedProducts = new List<ProductViewModel>();
                var review = new ProductReviewViewModel();
                var similarProducts = new List<ProductViewModel>();

                if (!ClaimUtility.IsColleauge(User))
                {
                    relatedProducts = _productService.GetListRelatedProduct(id);
                    properties = _productService.GetProperty(id);
                    //review = _productService.GetProductReview(id);
                    comments = _commentService.GetAllCommentForUser(id);
                    similarProducts = _productService.GetListSimilarProduct(id);
                    //
                    productPageData.Comments = comments;
                    productPageData.Properties = properties;
                    productPageData.RelatedProducts = relatedProducts;
                   // productPageData.Review = review;
                    productPageData.SimilarProducts = similarProducts;
                }



               
            return View(productPageData);

          
        }


        [Route("Product/CommentTab")]
        [HttpPost]
        public IActionResult CommentTab(long productId, string faTitle)
        {
            ViewBag.faTitle = faTitle;
            Tuple<List<CommentForUserViewModel>, List<ReviewRatingViewModel>, int> comment = _commentService.GetCommentForUser(productId, 4);
            ViewBag.commentcount = comment.Item3;
            ViewBag.productid = productId;
            ViewBag.CommentPageNumber = 1;
            ViewBag.Commentsort = 1;
            return View(comment);
        }

        [Route("Product/ReviewTab")]
        public IActionResult ReviewTab(long productId)
        {
       var  review = _productService.GetProductReview(productId);

            return View("Components/ReviewProductComponent/ReviewProduct", review);
        }
        [HttpPost]
        [Route("Product/CommentList")]
        public IActionResult CommentList(long productId, int pageNumber, int sort, int commentCount)
        {
            ViewBag.commentcount = commentCount;
            ViewBag.productid = productId;
            ViewBag.CommentPageNumber = pageNumber;
            ViewBag.Commentsort = sort;
            return View(_commentService.GetCommentsByFilter(productId, pageNumber, sort, 4));
        }




        public JsonResult GetProductPrice(int id)
        {
            var prices = _productService.GetProductPricesForChart(id);
            if (prices.Count > 0)
            {
                List<ReturnProductPricesChartViewModel> returndata = new List<ReturnProductPricesChartViewModel>();
                foreach (var item in prices.GroupBy(c => c.ProductOptionId))
                {
                    ReturnProductPricesChartViewModel returntemp = new ReturnProductPricesChartViewModel() { Color = item.FirstOrDefault().Color, productPrices = new List<ProductPricesChartViewModel>() };
                    for (var i = DateTime.Now.AddDays(-90); i < DateTime.Now.AddDays(-1); i = i.AddDays(1))
                    {
                        ProductPricesChartViewModel temp = item.ToList().Where(c => c.Date == i.GetDateForChart()).FirstOrDefault();
                        if (temp == null)
                        {
                            if (i.Date == DateTime.Now.AddDays(-90).Date)
                            {
                                temp = _productService.GetProductPriceForChartByProductOptionId(id, item.Key);
                                if (temp == null)
                                {
                                    temp = new ProductPricesChartViewModel
                                    {
                                        Date = i.GetDateForChart(),
                                        Price = 0,
                                        DiscountPercent = 0,
                                        IsAvailable = false,
                                        DiscountPrice = 0,
                                        ProductOptionId = item.Key,
                                    };
                                }
                            }
                            else
                            {
                                temp = new ProductPricesChartViewModel
                                {
                                    Date = i.GetDateForChart(),
                                    Price = returntemp.productPrices.LastOrDefault().Price,
                                    DiscountPercent = returntemp.productPrices.LastOrDefault().DiscountPercent,
                                    DiscountPrice = returntemp.productPrices.LastOrDefault().DiscountPrice,
                                    Guarantee = returntemp.productPrices.LastOrDefault().Guarantee,
                                    IsAvailable = returntemp.productPrices.LastOrDefault().IsAvailable,
                                    ProductOptionId = returntemp.productPrices.LastOrDefault().ProductOptionId,
                                    Seller = returntemp.productPrices.LastOrDefault().Seller
                                };


                            }
                        }

                        returntemp.productPrices.Add(temp);
                    }
                    returndata.Add(returntemp);
                }

                return Json(new { status = true, series = returndata });
            }
            else
            {
                return Json(new { statue = false });
            }

        }



        //[Authorize]
        [Route("AddComment")]
        public IActionResult AddComment(long id)
        {
            ProductDetailUserViewModel product = _productService.GetProductDetailUser(id, 0);

            return View(product);

        }
        [Route("Product3d/{id}")]
        public IActionResult ShowProduct3d(int id)
        {
            try
            {
                ViewBag.Id = id.ToString();
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "file3d/" + id, "index.html");

                var res = System.IO.File.ReadAllText(htmlFilePath);
                //Write your logic here 
                return PartialView("_ShowProduct3d", res);
            }
            catch (Exception e)
            {
                return PartialView("_ShowProduct3d", ":)");

            }

        }
    }
}
