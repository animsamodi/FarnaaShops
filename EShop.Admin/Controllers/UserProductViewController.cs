using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class UserProductViewController : BaseAdminController
    {
        private IUserProductViewService _UserProductViewService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        public UserProductViewController(IUserProductViewService UserProductViewService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService)
        {
            _UserProductViewService = UserProductViewService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
        }


        public ActionResult Index()
        {
            var res = _UserProductViewService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {
      

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(UserProductView model)
        {
            bool res = _UserProductViewService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _UserProductViewService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _UserProductViewService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            var data = _UserProductViewService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(UserProductView model)
        {

            bool res = _UserProductViewService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}