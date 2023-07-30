using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Site;
using EShop.Logging.AuditLog.Models;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ProductOtherPageController : BaseAdminController
    {
        private IProductOtherPageService _ProductOtherPageService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        private IImageUploadService _imageUploadService;

        public ProductOtherPageController(IProductOtherPageService ProductOtherPageService,
            ICategoryService categoryService, IBrandService brandService, Logging.AuditLog.IAuditService logger,
            IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) 
            : base(logger,contextAccessor)
        {
            _ProductOtherPageService = ProductOtherPageService;
            _categoryService = categoryService;
            _brandService = brandService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Index()
        {
            var res = _ProductOtherPageService.GetListForAdmin();
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

        public IActionResult Create(ProductOtherPage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
           
            if (model.BgImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.BgImageImg))
                {
                 //   model.BgImage = model.BgImageImg.SaveImage("", "wwwroot/uploads");
                    model.BgImage = _imageUploadService.Upload(model.BgImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("BgImageImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.SideImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.SideImageImg))
                {
                  //  model.SideImage = model.SideImageImg.SaveImage("", "wwwroot/uploads");
                    model.SideImage = _imageUploadService.Upload(model.SideImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("SideImageImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
             
 


            ProductOtherPage ProductOtherPage = new ProductOtherPage()
            {
                Order = model.Order,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Sort = model.Sort,
                Count = model.Count,
                BgImage = model.BgImage,
                SideImage = model.SideImage,
                IsActive = model.IsActive,
                Title = model.Title,
                Type = model.Type,
                Url = model.Url,

            };

            TempData["res"] = "faild";
            if (_ProductOtherPageService.Add(ProductOtherPage))
            {
                _logger.CreateAuditScope(new AuditLog<ProductOtherPage>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = ProductOtherPage,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _ProductOtherPageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            TempData["res"] = "faild";
            if (_ProductOtherPageService.Delete(data))
            {
                _logger.CreateAuditScope(new AuditLog<ProductOtherPage>()
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
            var data = _ProductOtherPageService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
       
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ProductOtherPage model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();


            if (!ModelState.IsValid)
                return View(model);



            if (model.BgImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.BgImageImg))
                {
                  //  model.BgImage = model.BgImageImg.SaveImage("", "wwwroot/uploads");
                    model.BgImage = _imageUploadService.Upload(model.BgImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("BgImageImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.SideImageImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.SideImageImg))
                {
                  //  model.SideImage = model.SideImageImg.SaveImage("", "wwwroot/uploads");
                    model.SideImage = _imageUploadService.Upload(model.SideImageImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("SideImageImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }


            ProductOtherPage ProductOtherPage = new ProductOtherPage()
            {
                Order = model.Order,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Sort = model.Sort,
                Count = model.Count,
                BgImage = model.BgImage,
                SideImage = model.SideImage,
                IsActive = model.IsActive,
                Title = model.Title,
                Type = model.Type,
                Url = model.Url,
                Id = model.Id
            };

            TempData["res"] = "faild";
            if (_ProductOtherPageService.Update(ProductOtherPage))
            {
                _logger.CreateAuditScope(new AuditLog<ProductOtherPage>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = ProductOtherPage,
                });
                TempData["res"] = "success";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}