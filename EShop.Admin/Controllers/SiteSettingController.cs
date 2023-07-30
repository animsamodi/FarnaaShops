using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SiteSettingController : BaseAdminController
    {
        private ISiteSettingService _siteSettingService;
        private IImageUploadService _imageUploadService;

        public SiteSettingController(ISiteSettingService siteSettingService,
            Logging.AuditLog.IAuditService logger, IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) : base(logger, contextAccessor)
        {
            _siteSettingService = siteSettingService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult Edit()
        {

            var data = _siteSettingService.GetDataByType(EnumTypeSystem.Farnaa);

            SiteSettingViewModel res = new SiteSettingViewModel()
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
                MetaTitle = data.MetaTitle,
                MetaDescription = data.MetaDescription,
                MetaKeywords = data.MetaKeywords,
                Canonical = data.Canonical,
                HeaderTag = data.HeaderTag,
                Schema = data.Schema,
                BlogMetaTitle = data.BlogMetaTitle,
                BlogMetaDescription = data.BlogMetaDescription,
                BlogMetaKeywords = data.BlogMetaKeywords,
                BlogCanonical = data.BlogCanonical,
                BlogHeaderTag = data.BlogHeaderTag,
                BlogSchema = data.BlogSchema,
                HomeText = data.HomeText,
                DefaultIpg = data.DefaultIpg,
                ShowApIpg = data.ShowApIpg,
                ShowKishIpg = data.ShowKishIpg,
                ShowZarinPalIpg = data.ShowZarinPalIpg,
                ShowMelatIpg = data.ShowMelatIpg,
                ShowMeliIpg = data.ShowMeliIpg,

                SearchBg = data.SearchBg

            };
            return View(res);
        }
        public ActionResult EditPlus()
        {

            var data = _siteSettingService.GetDataByType(EnumTypeSystem.FarnaaPlus);

            SiteSettingViewModel res = new SiteSettingViewModel()
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
                MetaTitle = data.MetaTitle,
                MetaDescription = data.MetaDescription,
                MetaKeywords = data.MetaKeywords,
                Canonical = data.Canonical,
                HeaderTag = data.HeaderTag,
                Schema = data.Schema,
                BlogMetaTitle = data.BlogMetaTitle,
                BlogMetaDescription = data.BlogMetaDescription,
                BlogMetaKeywords = data.BlogMetaKeywords,
                BlogCanonical = data.BlogCanonical,
                BlogHeaderTag = data.BlogHeaderTag,
                BlogSchema = data.BlogSchema,
                HomeText = data.HomeText,
                DefaultIpg = data.DefaultIpg,
                ShowApIpg = data.ShowApIpg,
                ShowKishIpg = data.ShowKishIpg,
                ShowZarinPalIpg = data.ShowZarinPalIpg,
                ShowMelatIpg = data.ShowMelatIpg,
                ShowMeliIpg = data.ShowMeliIpg,

                SearchBg = data.SearchBg

            };
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(SiteSettingViewModel model)
        {


            if (!ModelState.IsValid)
                return View(model);


            if (model.ImageFormFileWeb != null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFileWeb))
                {
                   // model.TopImageBannerWeb = model.ImageFormFileWeb.SaveImage("", "wwwroot/uploads", false);
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
                 //   model.TopImageBannerMobile = model.ImageFormFileMobile.SaveImage("", "wwwroot/uploads", false);
                    model.TopImageBannerMobile = _imageUploadService.Upload(model.ImageFormFileMobile, false);
                    ;
                }
                else
                {
                    ModelState.AddModelError("ImageFormFileMobile", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            if (model.ImageFormFileSearchBg!= null)
            {
                if (ImageSecurity.Imagevalidator(model.ImageFormFileSearchBg))
                {
                   // model.SearchBg = model.ImageFormFileSearchBg.SaveImage("", "wwwroot/uploads", false);
                    model.SearchBg = _imageUploadService.Upload(model.ImageFormFileSearchBg,false);
                    ;
                }
                else
                {
                    ModelState.AddModelError("ImageFormFileSearchBg", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }

            var siteSetting = _siteSettingService.FindById(model.Id);

            siteSetting.Id = model.Id;
            siteSetting.TopImageBannerWeb = model.TopImageBannerWeb;
            siteSetting.TopImageBannerWebTitle = model.TopImageBannerWebTitle;
            siteSetting.TopImageBannerMobile = model.TopImageBannerMobile;
            siteSetting.TopImageBannerMobileTitle = model.TopImageBannerMobileTitle;
            siteSetting.ShowTopImageBannerWeb = model.ShowTopImageBannerWeb;
            siteSetting.ShowTopImageBannerMobile = model.ShowTopImageBannerMobile;
            siteSetting.TopImageBannerWebUrl = model.TopImageBannerWebUrl;
            siteSetting.TopImageBannerMobileUrl = model.TopImageBannerMobileUrl;
            siteSetting.MetaTitle = model.MetaTitle;
            siteSetting.MetaDescription = model.MetaDescription;
            siteSetting.MetaKeywords = model.MetaKeywords;
            siteSetting.Canonical = model.Canonical;
            siteSetting.HeaderTag = model.HeaderTag;
            siteSetting.Schema = model.Schema;
            siteSetting.BlogMetaTitle = model.BlogMetaTitle;
            siteSetting.BlogMetaDescription = model.BlogMetaDescription;
            siteSetting.BlogMetaKeywords = model.BlogMetaKeywords;
            siteSetting.BlogCanonical = model.BlogCanonical;
            siteSetting.BlogHeaderTag = model.BlogHeaderTag;
            siteSetting.BlogSchema = model.BlogSchema;
            siteSetting.HomeText = model.HomeText;
            siteSetting.DefaultIpg = model.DefaultIpg;
            siteSetting.ShowApIpg = model.ShowApIpg;
            siteSetting.ShowKishIpg = model.ShowKishIpg;
            siteSetting.ShowMelatIpg = model.ShowMelatIpg;
            siteSetting.ShowZarinPalIpg = model.ShowZarinPalIpg;
            siteSetting.ShowMeliIpg = model.ShowMeliIpg;
            siteSetting.SearchBg = model.SearchBg;


            TempData["res"] = "faild";
            if (_siteSettingService.Update(siteSetting))
            {
                _logger.CreateAuditScope(new AuditLog<SiteSetting>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = siteSetting,
                });
                TempData["res"] = "success";
            }

            return View(model);
        }

    }




}