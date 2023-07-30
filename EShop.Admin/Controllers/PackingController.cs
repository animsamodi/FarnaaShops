using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Variety;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class PackingController : BaseAdminController
    {
        private IPackingService _PackingService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        readonly IVariantService _variantService;
 
        public PackingController(IPackingService PackingService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService, IVariantService variantService)
        {
            _PackingService = PackingService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
            _variantService = variantService;
        }


        public ActionResult Index()
        {
           


            var res = _PackingService.GetListForAdmin();

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

        public IActionResult Create(Packing model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();




            bool res = _PackingService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _PackingService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _PackingService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _PackingService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Packing model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);



            bool res = _PackingService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}