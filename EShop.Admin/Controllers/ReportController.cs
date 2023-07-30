using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class ReportController : BaseAdminController
    {
        public ActionResult Order()
        {
            return View();
        }
    }
    [Route("api/[controller]")]
    public class ReportFilteringController : Controller
    {
        private IReportService _reportService;

        public ReportFilteringController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet]
        public object GetOrder(DataSourceLoadOptions loadOptions)
        {
            var res = _reportService.GetOrderReport();
            return DataSourceLoader.Load(res, loadOptions);; 
        }
    }

 
   
}