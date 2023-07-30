using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ProductSeriController : BaseAdminController
    {
        private IProductSeriService _ProductSeriService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;

        public ProductSeriController(IProductSeriService ProductSeriService, ICategoryService categoryService, IBrandService brandService)
        {
            _ProductSeriService = ProductSeriService;
            _categoryService = categoryService;
            _brandService = brandService;
        }


        public ActionResult Index()
        {
            var res = _ProductSeriService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(ProductSeri model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
           
            
 
             
            bool res = _ProductSeriService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _ProductSeriService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _ProductSeriService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            var data = _ProductSeriService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ProductSeri model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();


            if (!ModelState.IsValid)
                return View(model);


             
            bool res = _ProductSeriService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public IActionResult GetSeriesByCategoryBrandId(int catId=0,int brandId = 0)
        {
            if (catId == 0 || brandId == 0)
                return Json(null);

            return Json(_ProductSeriService.GetSeriesByCategoryBrandId(catId, brandId));
        }

    }
}