using System;
using EndPoint.Web.Utilities;
using EShop.Core.Services.Interfaces.Seo;
using EShop.Core.ViewModels.Seo;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SeoController : BaseAdminController
    {
        private IRedirectService _redirectService;
        public SeoController(IRedirectService redirectService, IConfiguration configuration, Logging.AuditLog.IAuditService logger
            , IHttpContextAccessor contextAccessor) : base(logger, contextAccessor)
        {
            _redirectService = redirectService;
        }
        #region MainMenu
        public IActionResult Index() => View();

        public IActionResult RedirectList() => View(_redirectService.GetListRedirectForAdmin());
        public IActionResult CreateRedirect() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateRedirect(CreateRediretViewModel redirect)
        {
            var userId = ClaimUtility.GetUserId(User);

            if (!ModelState.IsValid)
                return View(redirect);
            if (_redirectService.RedirectIsExistOrNot(redirect.OldUrl))
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(RedirectList));
            }
            DataLayer.Entities.Seo.Redirect r = new DataLayer.Entities.Seo.Redirect
            {

                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                OldUrl = redirect.OldUrl,
                NewUrl = redirect.NewUrl,
                IsActive = redirect.IsActive,
                ChangeUserId = userId,
                TypeSystem = redirect.TypeSystem
            };

            TempData["res"] = "faild";
            if (_redirectService.AddRedirect(r))
            {
                _logger.CreateAuditScope(new AuditLog<DataLayer.Entities.Seo.Redirect>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = r,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("RedirectList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRedirect(int id)
        {
            var redirect = _redirectService.FindRedirectById(id);
            if (redirect == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(RedirectList));
            }
            TempData["res"] = "faild";
            if (_redirectService.DeleteRedirect(redirect))
            {
                _logger.CreateAuditScope(new AuditLog<DataLayer.Entities.Seo.Redirect>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = redirect,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(RedirectList));

        }


        public IActionResult EditRedirect(int id)
        {
            var redirect = _redirectService.FindRedirectById(id);
            if (redirect == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(RedirectList));
            }

            EditRedirectViewModel redirectvm = new EditRedirectViewModel()
            {
                Id = redirect.Id,
                OldUrl = redirect.OldUrl,
                NewUrl = redirect.NewUrl,
                IsActive = redirect.IsActive,
                TypeSystem = redirect.TypeSystem
                
            };
            return View(redirectvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRedirect(EditRedirectViewModel redirect)
        {
            if (!ModelState.IsValid)
                return View(redirect);

            var rd = new DataLayer.Entities.Seo.Redirect()
            {
                Id = redirect.Id,
                OldUrl = redirect.OldUrl,
                NewUrl = redirect.NewUrl,
                IsActive = redirect.IsActive,
                TypeSystem = redirect.TypeSystem
            };

            TempData["res"] = "faild";
            if (_redirectService.EditRedirect(rd))
            {
                _logger.CreateAuditScope(new AuditLog<DataLayer.Entities.Seo.Redirect>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = rd
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(RedirectList));
        }
        #endregion

    }
}

