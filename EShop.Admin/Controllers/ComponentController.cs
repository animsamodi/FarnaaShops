using System;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Web.Utilities;
using EShop.Admin.ViewModels.Components;
using EShop.Core.Services.Interfaces.Components;
using EShop.DataLayer.Entities.Components;
using EShop.Logging.AuditLog;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ComponentController : BaseAdminController
    {
        private IUiCircleComponentService _circleComponentService;
        public ComponentController(IUiCircleComponentService circleComponentService,IHttpContextAccessor contextAccessor
            ,IAuditService logger) : base(logger,contextAccessor)
        {
            _circleComponentService = circleComponentService;
        }

        public async Task<IActionResult> CircleComponents()
        {

            var components = await _circleComponentService.GetAll();

            var model = components.Select(c => new CircleComponentItemViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Url = c.Url,
                Order = c.Order,
                IsActive = c.IsActive
            });

            return View(model);
        }

        public IActionResult CreateCircleComponent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCircleComponent(CreateCircleComponentItemViewModel model)
        {
            var userId = ClaimUtility.GetUserId(User);

            if (!ModelState.IsValid)
                return View(model);

            UiCircleComponent component = new UiCircleComponent
            {
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                Url = model.Url,
                Title = model.Title,
                IsActive = model.IsActive,
                Order = model.Order,
                ChangeUserId = userId
            };
            _circleComponentService.Add(component);

            _logger.CreateAuditScope(new AuditLog<UiCircleComponent>()
            {
                Modifier = _userId,
                Action = Command.Create,
                Entite = component,
            });
          
            return RedirectToAction("CircleComponents");
        }

        public IActionResult UpdateCircleComponent(long id)
        {
            var component = _circleComponentService.GetById(id);
            if (component == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(CircleComponents));
            }

            CircleComponentItemViewModel model = new CircleComponentItemViewModel()
            {
                Id = component.Id,
                Title = component.Title,
                Url = component.Url,
                Order = component.Order,
                IsActive = component.IsActive
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCircleComponent(CircleComponentItemViewModel model)
        {
            var userId = ClaimUtility.GetUserId(User);

            if (!ModelState.IsValid)
                return View(model);

            var component = _circleComponentService.GetById(model.Id);
            if (component == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(CircleComponents));
            }


            component.SetIsActive(model.IsActive);
            component.SetTitle(model.Title);
            component.SetUrl(model.Url);
            component.SetOrder(model.Order);
            component.ChangeUserId = userId;
            
            component.LastUpdateDate = DateTime.Now;

            _logger.CreateAuditScope(new AuditLog<UiCircleComponent>()
            {
                Modifier = _userId,
                Action = Command.Update,
                Entite = component,
            });


            return RedirectToAction("CircleComponents");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCircleComponent(long id)
        {
            var component = _circleComponentService.GetById(id);
            if (component == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(CircleComponents));
            }
            _circleComponentService.Delete(component);
            return RedirectToAction(nameof(CircleComponents));
        }



    }
}
