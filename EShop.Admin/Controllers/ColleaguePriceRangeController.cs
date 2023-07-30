using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Variety;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ColleaguePriceRangeController : BaseAdminController
    {
        private IColleaguePriceRangeService _ColleaguePriceRangeService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService; 
        readonly IVariantService _variantService;
        private readonly IRecurringJobManager _recurringJobManager;

        public ColleaguePriceRangeController(IColleaguePriceRangeService ColleaguePriceRangeService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService, IVariantService variantService, IRecurringJobManager recurringJobManager)
        {
            _ColleaguePriceRangeService = ColleaguePriceRangeService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
            _variantService = variantService;
            _recurringJobManager = recurringJobManager;
        }


        public ActionResult Index()
        {
            try
            {
                _recurringJobManager.AddOrUpdate("ResetColleaugePrice", () => ResetCustomColleaugePrice(), Cron.Daily(18, 30));

            }
            catch (Exception e)
            {
             
            }


            var res = _ColleaguePriceRangeService.GetListForAdmin();

            return View(res);
        }
        public ActionResult ResetCustomPrice()
        {
            var res = ResetCustomColleaugePrice();
            if (res)
            {
                TempData["SuccessMessage"] = "عملیات با موفقیت انجام شد";
            }
            else
            {
                TempData["ErrorMessage"] = "بازه قیمتی غیر فعال است";

            }
            return RedirectToAction(nameof(Index));
        }

        public bool ResetCustomColleaugePrice()
        {
            var lstRange = _ColleaguePriceRangeService.GetListActiveRange();
            var res = _variantService.ResetCustomColleaugePrice(lstRange);
            return res;
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

        public IActionResult Create(ColleaguePriceRange model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();




            bool res = _ColleaguePriceRangeService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _ColleaguePriceRangeService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _ColleaguePriceRangeService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _ColleaguePriceRangeService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }
        public IActionResult UpdatePrice(int id)
        {
            var reng = _ColleaguePriceRangeService.FindById(id);
            if (reng != null && reng.IsActive)
            {
                var res = _variantService.UpdateColleaugePriceByRenge(reng.MinPrice, reng.MaxPrice, reng.ChangePrice);

                if (res)
                {
                    TempData["SuccessMessage"] = "عملیات با موفقیت انجام شد";
                }
                else
                {
                    TempData["ErrorMessage"] = "بازه قیمتی غیر فعال است";

                }
            }
            else
            {
                TempData["ErrorMessage"] = "خطا در انجام عملیان";
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ColleaguePriceRange model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);



            bool res = _ColleaguePriceRangeService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}