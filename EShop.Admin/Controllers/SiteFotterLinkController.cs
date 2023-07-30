using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SiteFotterLinkController : BaseAdminController
    {
        private ISiteFotterLinkService _SiteFotterLinkService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService; private IImageUploadService _imageUploadService;

        public SiteFotterLinkController(ISiteFotterLinkService SiteFotterLinkService,
           ICategoryService categoryService,
           IBrandService brandService,
           IProductSeriService productSeriService, IImageUploadService imageUploadService)
        {
            _SiteFotterLinkService = SiteFotterLinkService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Index()
        {
            var res = _SiteFotterLinkService.GetListForAdmin();
            return View(res);
        }
        public IActionResult Create()
        {
 

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(SiteFotterLink model)
        {
        
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



            bool res = _SiteFotterLinkService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _SiteFotterLinkService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _SiteFotterLinkService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
 

            var data = _SiteFotterLinkService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(SiteFotterLink model)
        {

            if (model.Type == EnumTypeFotterLink.Enamad || model.Type == EnumTypeFotterLink.SamanDehi|| model.Type == EnumTypeFotterLink.KasboKar)
            {
                var data = _SiteFotterLinkService.FindById(model.Id);
                data.IsActive = model.IsActive;
                bool res = _SiteFotterLinkService.Update(model);
                TempData["res"] = res ? "success" : "faild";

            }
            else
            {
      if (!ModelState.IsValid)
                return View(model);
            if (model.ImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageImg))
                {
                 //   model.Image = model.ImageImg.SaveImage("", "wwwroot/uploads");
                    model.Image = _imageUploadService.Upload(model.ImageImg);
                    ;
                    }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }


            bool res = _SiteFotterLinkService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            }

      
            return RedirectToAction(nameof(Index));
        }

    }
}