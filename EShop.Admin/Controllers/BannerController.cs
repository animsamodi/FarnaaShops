using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.Logging.AuditLog.Models;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class BannerController : BaseAdminController
    {
        readonly ISliderService _sliderService;
        IBannerService _bannerService; private IImageUploadService _imageUploadService;

        public BannerController(ISliderService sliderService, IBannerService bannerService ,IHttpContextAccessor contextAccessor
            , Logging.AuditLog.IAuditService logger, IImageUploadService imageUploadService) : base(logger,contextAccessor)
        {
            _sliderService = sliderService;
            _bannerService = bannerService;
            _imageUploadService = imageUploadService;
        }

        #region Banner
        public IActionResult BannerList()
        {
            return View(_bannerService.GetBannerForAdmin());
        }

        public IActionResult ChangeActive(int id)
        {
            TempData["res"] = _bannerService.ChangeActiveBanner(id) ? "success" : "faild";
            return RedirectToAction(nameof(BannerList));
        }

        public IActionResult EditBannerImage(int id)
        {
            BannerImage bannerImage = _sliderService.FindBannerImageById(id);
            if (bannerImage == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(BannerList));
            }

            EditBannerImageViewModel model = new EditBannerImageViewModel()
            {
                Title = bannerImage.Title,
                Link = bannerImage.Link,
                 Id = bannerImage.Id,
                CurrentImgName = bannerImage.ImageName,
                IsActive = bannerImage.IsActive,
                TypeSystem = bannerImage.TypeSystem
            };
      
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBannerImage(EditBannerImageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            string filename = model.CurrentImgName;
            if (model.Img != null)
            {
                if (ImageSecurity.Imagevalidator(model.Img))
                {
                    //filename.DeleteImage("wwwroot/uploads");
                    //filename = model.Img.SaveImage("", "wwwroot/uploads");
                    filename = _imageUploadService.Upload(model.Img);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Img", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }

            BannerImage bannerImage = new BannerImage()
            {
                Id = model.Id,
                Title = model.Title,
                ImageName = filename,
                 Link = model.Link,
                 IsActive = model.IsActive,
                 TypeSystem = model.TypeSystem
            };


            TempData["res"] = "faild";

            if (_sliderService.UpdateBannerImage(bannerImage))
            {
                _logger.CreateAuditScope(new AuditLog<BannerImage>()
                {
                    Entite = bannerImage,
                    Action = Command.Update,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(BannerList));
        }
        #endregion


        #region slider
        public IActionResult SliderList()
        {
            return View(_sliderService.GetSliderForAdmin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteSlider(int id)
        {
            Slider slider = _sliderService.FindSliderById(id);
            if (slider==null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(SliderList));
            }
            bool res = _sliderService.DeleteSlider(slider);

            if (res)
            {
                _logger.CreateAuditScope(new AuditLog<Slider>()
                {
                    Entite = slider,
                    Action = Command.Remove,
                    Modifier = _userId
                });
                slider.ImgName.DeleteImage("wwwroot/uploads");
                slider.ImgNameMobile.DeleteImage("wwwroot/uploads");
            }
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(SliderList));

        }
        public IActionResult CreateSlider() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateSlider(CreateSliderViewModel slid)
        {
            if (!ModelState.IsValid)
                return View(slid);
            string filename = "";
            string filenameMobile = "";
            if (ImageSecurity.Imagevalidator(slid.DesktopImg))
            {
                //filename = slid.DesktopImg.SaveImage("", "wwwroot/uploads");
                filename = _imageUploadService.Upload(slid.DesktopImg);
                ;
            }
            else
            {
                ModelState.AddModelError("DesktopImg", "لطفا یک فایل درست انتحاب کنید");
                return View(slid);
            }
            if (ImageSecurity.Imagevalidator(slid.MobileImg))
            {
                //filename = slid.MobileImg.SaveImage("", "wwwroot/uploads");
                filenameMobile = _imageUploadService.Upload(slid.MobileImg);
                ;
            }
            else
            {
                ModelState.AddModelError("MobileImg", "لطفا یک فایل درست انتحاب کنید");
                return View(slid);
            }

          
            Slider sl = new Slider()
            {
                ImgName=filename,
                ImgNameMobile=filenameMobile,
                Descrption=slid.Description,
                Link=slid.Link,
                LinkMobile= slid.Link,
                sort=slid.Sort,
                Type = slid.Type,
                TypeSystem = slid.TypeSystem
            };

            TempData["res"] = "faild";
            if (_sliderService.AddSlider(sl))
            {
                _logger.CreateAuditScope(new AuditLog<Slider>()
                {
                    Entite = sl,
                    Action = Command.Create,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(SliderList));
        }

        public IActionResult EditSlider(int id)
        {
            Slider slider = _sliderService.FindSliderById(id);
            if (slider==null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(SliderList));
            }
            EditSliderViewModel slvm = new EditSliderViewModel()
            {
                Description=slider.Descrption,
                Link=slider.Link,
                Sort=slider.sort,
                SliderId=slider.Id,
                CurrentImgName = slider.ImgName,
                CurrentImgNameMobile = slider.ImgNameMobile,
                Type = slider.Type,
                TypeSystem = slider.TypeSystem
            };
            return View(slvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditSlider(EditSliderViewModel slid)
        {
            if (!ModelState.IsValid)
                return View(slid);
            string filename = slid.CurrentImgName;
            string filenameMobile = slid.CurrentImgNameMobile;
            if (slid.DesktopImg != null)
            {
                if (ImageSecurity.Imagevalidator(slid.DesktopImg))
                {
                   // filename.DeleteImage("wwwroot/uploads");
                    //filename= slid.DesktopImg.SaveImage(filename, "wwwroot/uploads");
                    filename = _imageUploadService.Upload(slid.DesktopImg);
                    ;

                }
                else
                {
                    ModelState.AddModelError("DesktopImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(slid);
                }
            }
            if (slid.MobileImg != null)
            {
                if (ImageSecurity.Imagevalidator(slid.MobileImg))
                {
                    //filenameMobile.DeleteImage("wwwroot/uploads");
                    //slid.MobileImg.SaveImage(filenameMobile, "wwwroot/uploads");
                    filenameMobile = _imageUploadService.Upload(slid.MobileImg);
                }
                else
                {
                    ModelState.AddModelError("MobileImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(slid);
                }
            }
       

            Slider sl = new Slider()
            {
                Id = slid.SliderId,
                Descrption = slid.Description,
                ImgName = filename,
                ImgNameMobile = filenameMobile,
                sort = slid.Sort,
                Link = slid.Link,
                LinkMobile = slid.Link,
                Type = slid.Type,
                TypeSystem = slid.TypeSystem
            };
            TempData["res"] = "faild";
            if (_sliderService.UpdateSlider(sl))
            {
                _logger.CreateAuditScope(new AuditLog<Slider>()
                {
                    Entite = sl,
                    Action = Command.Update,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(SliderList));
        }
        #endregion

    }
}