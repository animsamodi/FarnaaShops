using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class FilterPropertyController : BaseAdminController
    {
        private IFilterPropertyService _FilterPropertyService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService; private IImageUploadService _imageUploadService;

        public FilterPropertyController(IFilterPropertyService FilterPropertyService,
           ICategoryService categoryService,
           IBrandService brandService,
           IProductSeriService productSeriService, IImageUploadService imageUploadService)
        {
            _FilterPropertyService = FilterPropertyService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Index()
        {
            var res = _FilterPropertyService.GetListForAdmin();
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

        public IActionResult Create(FilterProperty model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();
            if (model.ImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageImg))
                {
                   // model.Image = model.ImageImg.SaveImage("", "wwwroot/uploads");
                    model.Image = _imageUploadService.Upload(model.ImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }



            bool res = _FilterPropertyService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _FilterPropertyService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _FilterPropertyService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _FilterPropertyService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(FilterProperty model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageImg))
                {
                   // model.Image = model.ImageImg.SaveImage("", "wwwroot/uploads");
                    model.Image = _imageUploadService.Upload(model.ImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }


            bool res = _FilterPropertyService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}