using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class WarehouseController : BaseAdminController
    {
        private IWarehouseService _WarehouseService;

        public WarehouseController(IWarehouseService WarehouseService)
        {
            _WarehouseService = WarehouseService;
        }


        public ActionResult Index()
        {
            var res = _WarehouseService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Warehouse model)
        {
           
 
             
            bool res = _WarehouseService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _WarehouseService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _WarehouseService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
  
            var data = _WarehouseService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Warehouse model)
        {
            

            if (!ModelState.IsValid)
                return View(model);


             
            bool res = _WarehouseService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }

    }
}