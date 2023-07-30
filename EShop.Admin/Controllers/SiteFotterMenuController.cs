using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SiteFotterMenuController : BaseAdminController
    {
        private ISiteFotterMenuService _SiteFotterMenuService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        public SiteFotterMenuController(ISiteFotterMenuService SiteFotterMenuService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService)
        {
            _SiteFotterMenuService = SiteFotterMenuService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
        }


        public ActionResult Index()
        {
            var res = _SiteFotterMenuService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {


            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(SiteFotterMenu model)
        {
            if (!ModelState.IsValid)
                return View(model);



            bool res = _SiteFotterMenuService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _SiteFotterMenuService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _SiteFotterMenuService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {

            var data = _SiteFotterMenuService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(SiteFotterMenu model)
        {


            if (!ModelState.IsValid)
                return View(model);



            bool res = _SiteFotterMenuService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}