using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Variety;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class GuaranteeController : BaseAdminController
    {
        private IVariantService _variantService;

        public GuaranteeController(IVariantService variantService,IHttpContextAccessor contextAccessor,
            Logging.AuditLog.IAuditService logger) : base(logger,contextAccessor)
        {
            _variantService = variantService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var res = _variantService.GetListGuarantee();
            return DataSourceLoader.Load(res, loadOptions);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Post(string values)
        {
            var guarantee = new Guarantee();
            JsonConvert.PopulateObject(values, guarantee);
 

            if (!TryValidateModel(guarantee))
                return BadRequest("خطا");


            TempData["res"] = "faild";
            if (_variantService.AddGuarantee(guarantee))
            {
                _logger.CreateAuditScope(new AuditLog<Guarantee>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = guarantee,
                });
                TempData["res"] = "success";
            }            
            
            return Ok();
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]

        public IActionResult Put(int key, string values)
        {

            var guarantee = _variantService.GetGuaranteeById(key);

            JsonConvert.PopulateObject(values, guarantee);
            if (!TryValidateModel(guarantee))
                return BadRequest("خطا");

            TempData["res"] = "faild";
            if (_variantService.EditGuarantee(guarantee))
            {
                _logger.CreateAuditScope(new AuditLog<Guarantee>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = guarantee,
                });
                TempData["res"] = "success";
            }


            return Ok();
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]

        public void Delete(int key)
        {
            var guarantee = _variantService.GetGuaranteeById(key);

            TempData["res"] = "faild";
            if (_variantService.DeleteGuarantee(guarantee))
            {
                _logger.CreateAuditScope(new AuditLog<Guarantee>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = guarantee,
                });
                TempData["res"] = "success";
            }            
        }
    }





}