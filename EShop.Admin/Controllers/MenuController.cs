using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    
    public class MenuController : BaseAdminController
    {
        readonly IMainMenuService _mainMenuService;
        public MenuController(IMainMenuService mainMenuService, Logging.AuditLog.IAuditService logger
            ,IHttpContextAccessor contextAccessor) : base(logger,contextAccessor)
        {
            _mainMenuService = mainMenuService;
        }
        public ActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var res = _mainMenuService.GetMenuListForAdmin();
            return DataSourceLoader.Load(res, loadOptions);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Post(string values)
        {
            var menu = new MainMenu();
            JsonConvert.PopulateObject(values, menu);

            if (menu.ParentId != 0)
            {
                menu.Type = 1;
            }
            else
            {
                menu.ParentId = null;
            }

            if (!TryValidateModel(menu))
                return BadRequest("خطا");

            TempData["res"] = "faild";
            if (_mainMenuService.AddParentMenu(menu) > 0)
            {
                _logger.CreateAuditScope(new AuditLog<MainMenu>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = menu,
                });
                TempData["res"] = "success";
            }
            

            return Ok();
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]
        public IActionResult Put(int key, string values)
        {

            MainMenu menu = _mainMenuService.GetParentMenu(key);
            JsonConvert.PopulateObject(values, menu);
            if (!TryValidateModel(menu))
                return BadRequest("خطا");
            if (menu.ParentId != 0)
            {
                menu.Type = 1;
            }
            else
            {
                menu.ParentId = null;
            }

            TempData["res"] = "faild";
            if (_mainMenuService.EditParentMenu(menu))
            {
                _logger.CreateAuditScope(new AuditLog<MainMenu>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = menu,
                });
                TempData["res"] = "success";
            }

            return Ok();
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public void Delete(int key)
        {
            MainMenu menu = _mainMenuService.GetParentMenu(key);

            TempData["res"] = "faild";
            if (_mainMenuService.DeleteMenu(menu))
            {
                _logger.CreateAuditScope(new AuditLog<MainMenu>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = menu,
                });
                TempData["res"] = "success";
            }
        }

        public ActionResult CldrData()
        {
            return new CldrDataScriptBuilder()
                .SetCldrPath("~/wwwroot/cldr-data")
                .UseLocales(new[] { "fa" })
                .Build();
        }
    }



  

}