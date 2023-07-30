using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Product;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class CategoryBrandPageController : BaseAdminController
    {
        private ICategoryBrandPageService _categoryBrandPageService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;

        public CategoryBrandPageController(ICategoryBrandPageService categoryBrandPageService,IHttpContextAccessor contextAccessor
         ,   ICategoryService categoryService, IBrandService brandService, Logging.AuditLog.IAuditService logger
       
         ) :
            base(logger,contextAccessor)
        {
            _categoryBrandPageService = categoryBrandPageService;
            _categoryService = categoryService;
            _brandService = brandService;
        }


        public ActionResult Index()
        {
            var res = _categoryBrandPageService.GetListForAdmin();
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

        public IActionResult Create(CategoryBrandPage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            if (!ModelState.IsValid)
                return View(model);

            TempData["res"] = "faild";
            if (_categoryBrandPageService.Add(model))
            {
                _logger.CreateAuditScope(new AuditLog<CategoryBrandPage>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = model,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _categoryBrandPageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            TempData["res"] = "faild";
            if (_categoryBrandPageService.Delete(data))
            {
                _logger.CreateAuditScope(new AuditLog<CategoryBrandPage>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = data,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            var data = _categoryBrandPageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CategoryBrandPage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            if (!ModelState.IsValid)
                return View(model);

            TempData["res"] = "faild";
            if (_categoryBrandPageService.Update(model))
            {
                _logger.CreateAuditScope(new AuditLog<CategoryBrandPage>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = model,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }

    }




}