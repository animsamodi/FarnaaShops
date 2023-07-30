using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Enum;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ColleaugeSettingController : BaseAdminController
    {
        private IColleaugeSettingService _colleaugeSettingService;
        private IImageUploadService _imageUploadService;

        public ColleaugeSettingController(IColleaugeSettingService colleaugeSettingService, IImageUploadService imageUploadService)
        {
            _colleaugeSettingService = colleaugeSettingService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Edit()
        {

            var data = _colleaugeSettingService.GetEntityByType(EnumTypeSystem.Farnaa);
            ColleaugeSettingViewModel res = new ColleaugeSettingViewModel()
            {
                Id = data.Id,
                TopImageBannerWeb = data.TopImageBannerWeb,
                TopImageBannerWebTitle = data.TopImageBannerWebTitle,
                TopImageBannerMobile = data.TopImageBannerMobile,
                TopImageBannerMobileTitle = data.TopImageBannerMobileTitle,
                ShowTopImageBannerWeb = data.ShowTopImageBannerWeb,
                ShowTopImageBannerMobile = data.ShowTopImageBannerMobile,
                TopImageBannerMobileUrl = data.TopImageBannerMobileUrl,
                TopImageBannerWebUrl = data.TopImageBannerWebUrl,
                EndTime = data.EndTime,
                StartTime = data.StartTime,
                IsActive = data.IsActive


            };
            return View(res);
        }
        public ActionResult EditPlus()
        {

            var data = _colleaugeSettingService.GetEntityByType(EnumTypeSystem.FarnaaPlus);
            ColleaugeSettingViewModel res = new ColleaugeSettingViewModel()
            {
                Id = data.Id,
                TopImageBannerWeb = data.TopImageBannerWeb,
                TopImageBannerWebTitle = data.TopImageBannerWebTitle,
                TopImageBannerMobile = data.TopImageBannerMobile,
                TopImageBannerMobileTitle = data.TopImageBannerMobileTitle,
                ShowTopImageBannerWeb = data.ShowTopImageBannerWeb,
                ShowTopImageBannerMobile = data.ShowTopImageBannerMobile,
                TopImageBannerMobileUrl = data.TopImageBannerMobileUrl,
                TopImageBannerWebUrl = data.TopImageBannerWebUrl,
                EndTime = data.EndTime,
                StartTime = data.StartTime,
                IsActive = data.IsActive


            };
            return View(res);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ColleaugeSettingViewModel model)
        {
            

            if (!ModelState.IsValid)
                return View(model);


            if (model.ImageFormFileWeb != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFileWeb))
                {
                  //  model.TopImageBannerWeb = model.ImageFormFileWeb.SaveImage("", "wwwroot/uploads", false);
                    model.TopImageBannerWeb = _imageUploadService.Upload(model.ImageFormFileWeb, false);
                    ;
                }
                else
                {
                    ModelState.AddModelError("ImageFormFileWeb", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.ImageFormFileMobile != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFileMobile))
                {
                    //model.TopImageBannerMobile = model.ImageFormFileMobile.SaveImage("", "wwwroot/uploads", false);
                    model.TopImageBannerMobile = _imageUploadService.Upload(model.ImageFormFileMobile, false);
                    ;
                }
                else
                {
                    ModelState.AddModelError("ImageFormFileMobile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }

            var colleaugeSetting = _colleaugeSettingService.FindById(model.Id);

            colleaugeSetting.Id = model.Id;
            colleaugeSetting.TopImageBannerWeb = model.TopImageBannerWeb;
            colleaugeSetting.TopImageBannerWebTitle = model.TopImageBannerWebTitle;
            colleaugeSetting.TopImageBannerMobile = model.TopImageBannerMobile;
            colleaugeSetting.TopImageBannerMobileTitle = model.TopImageBannerMobileTitle;
            colleaugeSetting.ShowTopImageBannerWeb = model.ShowTopImageBannerWeb;
            colleaugeSetting.ShowTopImageBannerMobile = model.ShowTopImageBannerMobile;
            colleaugeSetting.TopImageBannerWebUrl = model.TopImageBannerWebUrl;
            colleaugeSetting.TopImageBannerMobileUrl = model.TopImageBannerMobileUrl;
            colleaugeSetting.IsActive = model.IsActive;
            colleaugeSetting.EndTime = model.EndTime;
            colleaugeSetting.StartTime = model.StartTime;



            bool res = _colleaugeSettingService.Update(colleaugeSetting);
            TempData["res"] = res ? "success" : "faild";
            return View(model);
        }

    }




}