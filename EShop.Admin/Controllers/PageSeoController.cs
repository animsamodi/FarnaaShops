using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Seri;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class PageSeoController : BaseAdminController
    {
        private IPageSeoService _PageSeoService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
         public PageSeoController(IPageSeoService PageSeoService,
           ICategoryService categoryService,
           IBrandService brandService,
           IProductSeriService productSeriService)
        {
            _PageSeoService = PageSeoService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
        }


        public ActionResult Index()
        {
            var res = _PageSeoService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(PageSeo model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();




            bool res = _PageSeoService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _PageSeoService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _PageSeoService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _PageSeoService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(PageSeo model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);


             
            bool res = _PageSeoService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}