using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Variety;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class PlusPriceRangeController : BaseAdminController
    {
        private IPlusPriceRangeService _PlusPriceRangeService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        readonly IVariantService _variantService;
        private readonly IRecurringJobManager _recurringJobManager;

        public PlusPriceRangeController(IPlusPriceRangeService PlusPriceRangeService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService, IVariantService variantService, IRecurringJobManager recurringJobManager)
        {
            _PlusPriceRangeService = PlusPriceRangeService;
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
                _recurringJobManager.AddOrUpdate("ResetColleaugePrice", () => ResetCustomPlusPrice(), Cron.Daily(18, 30));

            }
            catch (Exception e)
            {
             
            }


            var res = _PlusPriceRangeService.GetListForAdmin();

            return View(res);
        }
        public ActionResult ResetCustomPrice()
        {
            var res = ResetCustomPlusPrice();
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

        public bool ResetCustomPlusPrice()
        {
            var lstRange = _PlusPriceRangeService.GetListActiveRange();
            var res = _variantService.ResetCustomPlusPrice(lstRange);
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

        public IActionResult Create(PlusPriceRange model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();




            bool res = _PlusPriceRangeService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _PlusPriceRangeService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _PlusPriceRangeService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _PlusPriceRangeService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }
        public IActionResult UpdatePrice(int id)
        {
            var reng = _PlusPriceRangeService.FindById(id);
            if (reng != null && reng.IsActive)
            {
                var res = _variantService.UpdatePlusPriceByRenge(reng.MinPrice, reng.MaxPrice, reng.ChangePricePercent);

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

        public IActionResult Edit(PlusPriceRange model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);



            bool res = _PlusPriceRangeService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}