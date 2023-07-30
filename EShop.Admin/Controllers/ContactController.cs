using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ContactController : BaseAdminController
    {
        private IContactService _ContactService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        public ContactController(IContactService ContactService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService)
        {
            _ContactService = ContactService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
        }


        public ActionResult Index()
        {
            var res = _ContactService.GetListForAdmin();
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

        public IActionResult Create(Contact model)
        {
            bool res = _ContactService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _ContactService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _ContactService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            var data = _ContactService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Contact model)
        {

            bool res = _ContactService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}