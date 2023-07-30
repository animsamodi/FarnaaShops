using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ProductPricePageController : BaseAdminController
    {
        private IProductPricePageService _ProductPricePageService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
         public ProductPricePageController(IProductPricePageService ProductPricePageService,
           ICategoryService categoryService,
           IBrandService brandService,
           IProductSeriService productSeriService)
        {
            _ProductPricePageService = ProductPricePageService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
        }


        public ActionResult Index()
        {
            var res = _ProductPricePageService.GetListForAdmin();
            foreach (var productPricePage in res)
            {
                var baseUrl = "https://farnaa.com/PriceList/";
                if (productPricePage.Category != null)
                    baseUrl += productPricePage.Category.EnTitle.ToUrlFormat();
                if (productPricePage.Brand != null)
                    baseUrl +="/"+ productPricePage.Brand.EnTitle.ToUrlFormat();
                if (productPricePage.ProductSeri != null)
                    baseUrl += "/" + productPricePage.ProductSeri.EnTitle.ToUrlFormat();

                productPricePage.Url = baseUrl;
            }
            return View(res);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(ProductPricePage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();




            bool res = _ProductPricePageService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _ProductPricePageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _ProductPricePageService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _ProductPricePageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ProductPricePage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);


             
            bool res = _ProductPricePageService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}