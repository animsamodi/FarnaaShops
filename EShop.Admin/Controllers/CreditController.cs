using System.Collections.Generic;
using AutoMapper;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    
    public class CreditController : BaseAdminController
    {
        readonly ICreditService _creditService;
        private IMapper _mapper;
        public CreditController(ICreditService creditService, IMapper mapper
            ,IHttpContextAccessor contextAccessor, Logging.AuditLog.IAuditService logger) : base(logger,contextAccessor)
        {
            _creditService = creditService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CreditList(CreditSearchViewModel search)
        {
            var content = _creditService.GetListForAdmin(search,25);
            ViewBag.count = content.Item1;
 
            ViewBag.PageNumber = search.Pagenumber;

            return View("_CreditList", content.Item2);
        }
        public IActionResult Detail(long id)
        {
            var data = _creditService.FindById(id);

            if (data == null)
                return RedirectToAction("Index");

            if (data.RealLegal == EnumRealLegal.Real)
            {
                var res = _mapper.Map<CreditHaghighiViewModel>(data);
                res.Partners = _mapper.Map<List<CreditPartnerViewModel>>(data.CreditPartners);
                res.Accounts = _mapper.Map<List<CreditAccountViewModel>>(data.CreditAccounts);
                res.Documents = _mapper.Map<List<CreditDocumentViewModel>>(data.CreditDocument);
                return View(  "DetailReal", res);

            }
            else
            {
                var res = _mapper.Map<CreditHoghughiViewModel>(data);
                res.Partners = _mapper.Map<List<CreditPartnerViewModel>>(data.CreditPartners);
                res.Accounts = _mapper.Map<List<CreditAccountViewModel>>(data.CreditAccounts);
                res.Documents = _mapper.Map<List<CreditDocumentViewModel>>(data.CreditDocument);
                return View("DetailLegal", res);
            }
        }
        public IActionResult EditStatus(long id,string adminMessage, EnumCreditStatus creditStatus,string CreditExpDate,int AcceptPrice=0)
        {
            var res = _creditService.ChangeAdminStatus(id, adminMessage, creditStatus, CreditExpDate, AcceptPrice);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("Index");
        }


        #region Bill

        public ActionResult ListBill()
        {
            return View();
        }

        public IActionResult CreditBillList(CreditBillSearchViewModel search)
        {
          
            var content = _creditService.GetAllCreditBill(search, 25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;

            return View("_CreditBillList", content.Item2);
        }
        public IActionResult DetailBill(long id)
        {
            var data = _creditService.FindCreditBillForAdminById(id);

            if (data == null)
                return RedirectToAction("CreditList");

           
                 return View("DetailBill", data);

             
        }
        public IActionResult EditStatusBill(long id, string adminMessage, EnumCreditBillStatus Status,  int ConfirmPrice = 0)
        {
            TempData["res"] = "faild";
            if (_creditService.ChangeBillAdminStatus(id, adminMessage, Status, ConfirmPrice))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = id,
                });
                TempData["res"] = "success";
            }
            return RedirectToAction("ListBill");
        }

        #endregion

    }





}