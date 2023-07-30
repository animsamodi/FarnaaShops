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
    public class CategotyMainController : BaseAdminController
    {
        private ICategoryMainService _categoryMainService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        private IImageUploadService _imageUploadService;

        public CategotyMainController(  ICategoryService categoryService, IBrandService brandService, 
            ICategoryMainService categoryMainService, Logging.AuditLog.IAuditService logger
            ,IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) : base(logger,contextAccessor)
        {
             _categoryService = categoryService;
            _brandService = brandService;
            _categoryMainService = categoryMainService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Index()
        {
            var res = _categoryMainService.GetListForAdmin();
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

        public IActionResult Create(CreateCategotyMainViewModel model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
           
            if (model.ImageFormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFile))
                {
                    //model.Image = model.ImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.Image = _imageUploadService.Upload(model.ImageFormFile);
                    ;
                }
                else
                {
                    ModelState.AddModelError("ImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }




            CategotyMain categotyMain = new CategotyMain
            {
                BrandId = model.BrandId,
                Type = model.Type,
                CategoryId = model.CategoryId,
                Color = model.Color,
                EnTitle = model.EnTitle,
                IsActive = model.IsActive,
                Image = model.Image,
                Title = model.Title,
                Order = model.Order,
                Text = model.Text,
                FAQSchema = model.FAQSchema,
                TypeSystem = model.TypeSystem

            };


            TempData["res"] = "faild";
            if (_categoryMainService.Add(categotyMain))
            {
                _logger.CreateAuditScope(new AuditLog<CategotyMain>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = categotyMain,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var data = _categoryMainService.FindSliderById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            TempData["res"] = "faild";
            if (_categoryMainService.Delete(data))
            {
                _logger.CreateAuditScope(new AuditLog<CategotyMain>()
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
            var data = _categoryMainService.FindSliderById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            CreateCategotyMainViewModel res = new CreateCategotyMainViewModel()
            {
          BrandId = data.BrandId,
          CategoryId = data.CategoryId,
          Color = data.Color,
          EnTitle = data.EnTitle,
          Id = data.Id,
          Image = data.Image,
          IsActive = data.IsActive,
          Order = data.Order,
          Title = data.Title,
          Type = data.Type,
          Text = data.Text,
          FAQSchema = data.FAQSchema,
          TypeSystem = data.TypeSystem

            };
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CreateCategotyMainViewModel model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();


            if (!ModelState.IsValid)
                return View(model);


            if (model.ImageFormFile != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFile))
                {
                    //model.Image = model.ImageFormFile.SaveImage("", "wwwroot/uploads");
                    model.Image = _imageUploadService.Upload(model.ImageFormFile);
                  
                }
                else
                {
                    ModelState.AddModelError("ImageFormFile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }




            CategotyMain categotyMain = new CategotyMain
            {
                BrandId = model.BrandId,
                Type = model.Type,
                CategoryId = model.CategoryId,
                Color = model.Color,
                EnTitle = model.EnTitle,
                IsActive = model.IsActive,
                Image = model.Image,
                Title = model.Title,
                Order = model.Order,
                Id = model.Id,
                Text = model.Text,
                TypeSystem = model.TypeSystem
                
            };

            TempData["res"] = "faild";
            if (_categoryMainService.Update(categotyMain))
            {
                _logger.CreateAuditScope(new AuditLog<CategotyMain>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = categotyMain,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }

    }




}