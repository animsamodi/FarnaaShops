using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class AuditController : BaseAdminController
    {
        private IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object GetList(DataSourceLoadOptions loadOptions)
        {
            var res = _auditService.GetListAudits(loadOptions.Skip,loadOptions.Take);
            var data =  DataSourceLoader.Load(res, loadOptions); ;
            return data;
        }
    }
 

 
   
}