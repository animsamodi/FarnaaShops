using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.Logging.AuditLog.Models;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class IndexLayoutController : BaseAdminController
    {
        private IIndexLayoutService _indexLayoutService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        private IImageUploadService _imageUploadService;

        public IndexLayoutController(IIndexLayoutService indexLayoutService,
            ICategoryService categoryService, IBrandService brandService, Logging.AuditLog.IAuditService logger,
            IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) 
            : base(logger,contextAccessor)
        {
            _indexLayoutService = indexLayoutService;
            _categoryService = categoryService;
            _brandService = brandService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Index()
        {
            var res = _indexLayoutService.GetListForAdmin();
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

        public IActionResult Create(CreateIndexLayoutViewModel model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
           
            if (model.Image1FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image1FormFile))
                {
                    //model.Image1 = model.Image1FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image1 = _imageUploadService.Upload(model.Image1FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image1FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image2FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image2FormFile))
                {
                   // model.Image2 = model.Image2FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image2 = _imageUploadService.Upload(model.Image2FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image2FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image3FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image3FormFile))
                {
                    //model.Image3 = model.Image3FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image3 = _imageUploadService.Upload(model.Image3FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image3FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image4FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image4FormFile))
                {
                    //model.Image4 = model.Image4FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image4 = _imageUploadService.Upload(model.Image4FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image4FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.BgImageFormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.BgImageFormFile))
                {
                    //model.BgImage = model.BgImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.BgImage = _imageUploadService.Upload(model.BgImageFormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("BgImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.SideImageFormFile!= null)
            {
                if (ImageSecurity.Imagevalidator(model.SideImageFormFile))
                {
                  //  model.SideImage = model.SideImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.SideImage = _imageUploadService.Upload(model.SideImageFormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("SideImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
 


            IndexLayout indexLayout = new IndexLayout()
            {
                Order = model.Order,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Sort = model.Sort,
                Count = model.Count,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
                Image4 = model.Image4,
                ImageUrl1 = model.ImageUrl1,
                ImageUrl2 = model.ImageUrl2,
                ImageUrl3 = model.ImageUrl3,
                ImageUrl4 = model.ImageUrl4,
                BgImage = model.BgImage,
                SideImage = model.SideImage,
                IsActive = model.IsActive,
                Title = model.Title,
                Type = model.Type,
                Url = model.Url,
                TypeSystem = model.TypeSystem

            };

            TempData["res"] = "faild";
            if (_indexLayoutService.Add(indexLayout))
            {
                _logger.CreateAuditScope(new AuditLog<IndexLayout>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = indexLayout,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }


         public IActionResult Delete(int id)
        {
            var data = _indexLayoutService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }

            TempData["res"] = "faild";
            if (_indexLayoutService.Delete(data))
            {
                _logger.CreateAuditScope(new AuditLog<IndexLayout>()
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
            var data = _indexLayoutService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            CreateIndexLayoutViewModel res = new CreateIndexLayoutViewModel()
            {
             Url = data.Url,
             BrandId = data.BrandId,
             CategoryId = data.CategoryId,
             Count = data.Count,
             Id = data.Id,
             Image1 = data.Image1,
             Image2 = data.Image2,
             Image3 = data.Image3,
             Image4 = data.Image4,
             ImageUrl1 = data.ImageUrl1,
             ImageUrl2 = data.ImageUrl2,
             ImageUrl3 = data.ImageUrl3,
             ImageUrl4 = data.ImageUrl4,
             BgImage = data.BgImage,
             SideImage = data.SideImage,
                IsActive = data.IsActive,
             Order = data.Order,
             Sort = data.Sort,
             Title = data.Title,
             Type = data.Type,
             TypeSystem = data.TypeSystem
            };
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CreateIndexLayoutViewModel model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();


            if (!ModelState.IsValid)
                return View(model);


            if (model.Image1FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image1FormFile))
                {
                   // model.Image1 = model.Image1FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image1 = _imageUploadService.Upload(model.Image1FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image1FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image2FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image2FormFile))
                {
                   // model.Image2 = model.Image2FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image2 = _imageUploadService.Upload(model.Image2FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image2FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image3FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image3FormFile))
                {
                    //model.Image3 = model.Image3FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image3 = _imageUploadService.Upload(model.Image3FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image3FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.Image4FormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.Image4FormFile))
                {
                    //model.Image4 = model.Image4FormFile.SaveImage("", "wwwroot/uploads");
                    model.Image4 = _imageUploadService.Upload(model.Image4FormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image4FormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.BgImageFormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.BgImageFormFile))
                {
                   // model.BgImage = model.BgImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.BgImage = _imageUploadService.Upload(model.BgImageFormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("BgImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.SideImageFormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.SideImageFormFile))
                {
                   // model.SideImage = model.SideImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.SideImage = _imageUploadService.Upload(model.SideImageFormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("SideImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            IndexLayout indexLayout = new IndexLayout()
            {
                Order = model.Order,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Sort = model.Sort,
                Count = model.Count,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
                Image4 = model.Image4,
                ImageUrl1 = model.ImageUrl1,
                ImageUrl2 = model.ImageUrl2,
                ImageUrl3 = model.ImageUrl3,
                ImageUrl4 = model.ImageUrl4,
                BgImage = model.BgImage,
                SideImage = model.SideImage,
                IsActive = model.IsActive,
                Title = model.Title,
                Type = model.Type,
                Url = model.Url,
                Id = model.Id,
                TypeSystem = model.TypeSystem
            };

            TempData["res"] = "faild";
            if (_indexLayoutService.Update(indexLayout))
            {
                _logger.CreateAuditScope(new AuditLog<IndexLayout>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = indexLayout,
                });
                TempData["res"] = "success";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}