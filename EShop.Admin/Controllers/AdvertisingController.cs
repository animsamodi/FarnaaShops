using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
    public class AdvertisingController : BaseAdminController
    {
        readonly ICategoryService _categoryService;
        readonly IProductService _productService;
        readonly IBrandService _brandService;
        readonly IPropertyService _propertyService;
        readonly IGalleryService _galleryService;
        readonly IReviewService _reviewService;
        readonly IAttributeRatingService _attributeRatingService;
        readonly IVariantService _variantService;
        public AdvertisingController(ICategoryService categoryService,
            IProductService productService, IBrandService brandService, IGalleryService galleryService, IReviewService reviewService, IAttributeRatingService attributeRatingService, IPropertyService propertyService, IVariantService variantService)
        {

            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _galleryService = galleryService;
            _reviewService = reviewService;
            _attributeRatingService = attributeRatingService;
            _propertyService = propertyService;
            _variantService = variantService;
        }



        #region Product
        public IActionResult ProductListContainer()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            return View("~/Views/Advertising/ProductListContainer.cshtml");
        }

        public IActionResult ProductList(string searchtext = "", int brnad = 0, int category = 0, int state = 1, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdmin(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/Advertising/ProductList.cshtml", content.Item2);
        }

        public IActionResult ProductListDiscountContainer()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            return View("~/Views/Advertising/ProductListContainerDiscount.cshtml");
        }

        public IActionResult ProductListDiscount(string searchtext = "", int brnad = 0, int category = 0, int state = 1, int pagenumber = 1)
        {
            var content = _variantService.GetProductsVariantDiscountForAdmin(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/Advertising/ProductListDiscount.cshtml", content.Item2);
        }



        #endregion















    }
}