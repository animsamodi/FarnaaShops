using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EShop.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class CreditController : Controller
    {
        private ICreditService _creditService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper; private readonly IAddressService _addressService;
        private IUserService _userService;

        public CreditController(ICreditService creditService, IHostingEnvironment hostingEnvironment, IConfiguration config, IMapper mapper, IAddressService addressService, IUserService userService)
        {
            _creditService = creditService;
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            _mapper = mapper;
            _addressService = addressService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);

            var res =
                _creditService.GetListForUser(userId);

            return View(res);
        }
    
        [Route("Credit/Real")]
        public IActionResult RequestReal()

        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);
            var user = _userService.GetUserById(userId);
            if (user is { IsHoghughi: true })
            {
                TempData["Message"] = "برای ثبت درخواست اعتباری حقیقی لطفا ابتدا پروفایل خود را بر اساس کاربر حقیقی تکمیل نمایید";
                return RedirectToAction("EditProfile", "Profile");
            }
            bool openRequest = _creditService.CheckOpenRequest(userId);
            if (openRequest)
            {
                TempData["Message"]= "کاربر گرامی شما از قبل درخواست ثبت شده دارید. تا مشخص نشده وضعیت درخواست قبلی,نمیتوانید درخواست جدیدی ثبت نکنید."  ;
                return RedirectToAction("Index");
            }
            ViewBag.FileType = _creditService.GetTypeFiles(EnumRealLegal.Real);
            return View();
        }

        [HttpPost]
        [Route("Credit/Real")]
        public async Task<IActionResult> RequestReal(CreditHaghighiViewModel model)
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);

            bool openRequest = _creditService.CheckOpenRequest(userId);
            if (openRequest)
            {
                TempData["Message"] = "کاربر گرامی شما از قبل درخواست ثبت شده دارید. تا مشخص نشده وضعیت درخواست قبلی,نمیتوانید درخواست جدیدی ثبت نکنید.";
                return RedirectToAction("Index");
            }
            ViewBag.FileType = _creditService.GetTypeFiles(EnumRealLegal.Real);


            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //        _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            if (model.NoeKharid != EnumNoeKharid.Naghdi && (model.ZemanatBankiMablagh == null || model.ZemanatBankiMablagh == 0) && (model.VasigheMelkiMablagh == null || model.VasigheMelkiMablagh == 0))
            {
                ModelState.AddModelError("ZemanatBankiMablagh", "لطفا یکی از موارد ضمانت را وارد کنید");
                TempData["TazminError"] = "لطفا یکی از موارد ضمانت را وارد کنید";
            }


            //if (_addressService.CheckBlockPostalCode(model.CodePostiNeshaniMahaleKar))
            //    ModelState.AddModelError("CodePostiNeshaniMahaleKar", "لطفا کد پستی صحیح وارد کنید");
            if (!model.NationalNumber.IsValidNationalCode())
                ModelState.AddModelError("NationalNumber", "لطفا شماره ی ملی صحیح وارد کنید");
            if (!model.BDate.IsValidBDate())
                ModelState.AddModelError("BDate", "لطفاً تاریخ تولد صحیح را وارد نمایید");

            if (model.Partners != null && model.Partners.Any())
            {
                var TotalPercent = model.Partners.Sum(partner => partner.Darsad);

                if (TotalPercent != 100)
                {
                    ModelState.AddModelError("TotalPercent", "جمع درصد های شرکا باید به 100 درصد برسد");
                    TempData["ParentError"] = "جمع درصد های شرکا باید به 100 درصد برسد";
                }

            }
            //
            var counter = 0;
            foreach (var document in model.Documents)
            {
                if (document.FormFile != null)
                {
                    if (!ImageSecurity.Imagevalidator(document.FormFile))
                    {
                        ModelState.AddModelError($"Documents[{counter}].FormFile", "لطفا یک فایل درست انتحاب کنید");
                        return View(model);
                    }
                }

                counter++;
            }
            //

            if (!ModelState.IsValid)
                return View(model);

            var credit = _mapper.Map<Credit>(model);

            if (model.Accounts != null)
            {
                var accounts = _mapper.Map<List<CreditAccount>>(model.Accounts);


                credit.CreditAccounts = accounts;
            }
            if (model.Partners != null)
            {
                var partners = _mapper.Map<List<CreditPartner>>(model.Partners);

                credit.CreditPartners = partners;
            }
            //
            if (model.Documents != null)
            {
                List<CreditDocument> documents = new List<CreditDocument>();
                foreach (var document in model.Documents)
                {
                    if (document.FormFile != null)
                    {

                        var filename = document.FormFile.SaveImage("", "wwwroot/uploads", false);
                        documents.Add(new CreditDocument
                        {
                            CreditDocumentTypeId = document.CreditDocumentTypeId,
                            File = filename
                        });


                    }
                }

                credit.CreditDocument = documents;
            }

 
            credit.RealLegal = EnumRealLegal.Real;
            credit.UserId = userId;
            credit.CreditStatus = EnumCreditStatus.Wating;
            credit.TrakingCode = CodeGenerator.CreditTrakingCodeCodeGenerator();
            var res = _creditService.AddWhitRelated(credit);
            if (res)
            {
                TempData["success"] =
                    "همراه ارجمند فرنا، درخواست شما در پلتفرم فرنا، با موفقیت ثبت گردید. منتظر پاسخ همکاران باشید.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
      
        [Route("Credit/Legal")]
        public IActionResult RequestLegal()
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);

            bool openRequest = _creditService.CheckOpenRequest(userId);
            if (openRequest)
            {
                TempData["Message"] = "کاربر گرامی شما از قبل درخواست ثبت شده دارید. تا مشخص نشده وضعیت درخواست قبلی,نمیتوانید درخواست جدیدی ثبت نکنید.";
                return RedirectToAction("Index");
            }
             var user = _userService.GetUserById(userId);
            if (user is { IsHoghughi: false })
            {
                TempData["Message"] = "برای ثبت درخواست اعتباری حقوقی لطفا ابتدا پروفایل خود را بر اساس کاربر حقوقی تکمیل نمایید";
                return RedirectToAction("EditProfile", "Profile");
            }

            ViewBag.FileType = _creditService.GetTypeFiles(EnumRealLegal.Legal);

            return View();
        }

        [HttpPost]
        [Route("Credit/Legal")]
        public async Task<IActionResult> RequestLegal(CreditHoghughiViewModel model)
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);

            bool openRequest = _creditService.CheckOpenRequest(userId);
            if (openRequest)
            {
                TempData["Message"] = "کاربر گرامی شما از قبل درخواست ثبت شده دارید. تا مشخص نشده وضعیت درخواست قبلی,نمیتوانید درخواست جدیدی ثبت نکنید.";
                return RedirectToAction("Index");
            }
            ViewBag.FileType = _creditService.GetTypeFiles(EnumRealLegal.Legal);


            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //        _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            if (model.NoeKharid != EnumNoeKharid.Naghdi && (model.ZemanatBankiMablagh == null || model.ZemanatBankiMablagh == 0) && (model.VasigheMelkiMablagh == null || model.VasigheMelkiMablagh == 0))
            {
                ModelState.AddModelError("ZemanatBankiMablagh", "لطفا یکی از موارد ضمانت را وارد کنید");
                TempData["TazminError"] = "لطفا یکی از موارد ضمانت را وارد کنید";
            }


            //if (_addressService.CheckBlockPostalCode(model.CodePostiNeshaniMahaleKar))
             //   ModelState.AddModelError("CodePostiNeshaniMahaleKar", "لطفا کد پستی صحیح وارد کنید");
            if (!model.NationalNumber.IsValidNationalCode())
                ModelState.AddModelError("NationalNumber", "لطفا شماره ی ملی صحیح وارد کنید");
            if (!model.BDate.IsValidBDate())
                ModelState.AddModelError("BDate", "لطفاً تاریخ تولد صحیح را وارد نمایید");

            if (model.Partners != null && model.Partners.Any())
            {
                var TotalPercent = model.Partners.Sum(partner => partner.Darsad);

                if (TotalPercent != 100)
                {
                    ModelState.AddModelError("TotalPercent", "جمع درصد های شرکا باید به 100 درصد برسد");
                    TempData["ParentError"] = "جمع درصد های شرکا باید به 100 درصد برسد";
                }

            }
            //
            var counter = 0;
            foreach (var document in model.Documents)
            {
                if (document.FormFile != null)
                {
                    if (!ImageSecurity.Imagevalidator(document.FormFile))
                    {
                        ModelState.AddModelError($"Documents[{counter}].FormFile", "لطفا یک فایل درست انتحاب کنید");
                        return View(model);
                    }
                }

                counter++;
            }
            //

            if (!ModelState.IsValid)
                return View(model);

            var credit = _mapper.Map<Credit>(model);

            if (model.Accounts != null)
            {
                var accounts = _mapper.Map<List<CreditAccount>>(model.Accounts);


                credit.CreditAccounts = accounts;
            }
            if (model.Partners != null)
            {
                var partners = _mapper.Map<List<CreditPartner>>(model.Partners);

                credit.CreditPartners = partners;
            }
            //
            if (model.Documents != null)
            {
                List<CreditDocument> documents = new List<CreditDocument>();
                foreach (var document in model.Documents)
                {
                    if (document.FormFile != null)
                    {

                        var filename = document.FormFile.SaveImage("", "wwwroot/uploads", false);
                        documents.Add(new CreditDocument
                        {
                            CreditDocumentTypeId = document.CreditDocumentTypeId,
                            File = filename
                        });


                    }
                }

                credit.CreditDocument = documents;
            }

 
            credit.RealLegal = EnumRealLegal.Legal;
            credit.UserId = userId;
            credit.CreditStatus = EnumCreditStatus.Wating;
            credit.TrakingCode = CodeGenerator.CreditTrakingCodeCodeGenerator();
            var res = _creditService.AddWhitRelated(credit);
            if (res)
            {
                TempData["success"] =
                    "همراه ارجمند فرنا، درخواست شما در پلتفرم فرنا، با موفقیت ثبت گردید. منتظر پاسخ همکاران باشید.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        #region CreditBill
        public IActionResult ListCreditBill()
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);

            var res =
                _creditService.GetAllUserCreditBillByUserId(userId);

            return View(res);
        }

        [Route("Credit/Bill")]
        public IActionResult RequestCreditBill()
        {
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);
            var res = _creditService.GetAllUserCreditBillByUserId(userId);
            return View();
        }
        [HttpPost]
        [Route("Credit/Bill")]
        public async Task<IActionResult> RequestCreditBillAsync(CreditBillViewModel model)
        {
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //            _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }


            //}
            //
            long userId = Convert.ToInt64(User.FindFirst("userid").Value);
 
            //


            if (!ModelState.IsValid)
                return View(model);

            if (model.FormFile != null)
                model.Image = model.FormFile.SaveImage("", "wwwroot/uploads",false);


            var bill = _mapper.Map<CreditBill>(model);
            //
            bill.Status = EnumCreditBillStatus.Wait;
            bill.DatePay = model.PrDatePay.PersianToEnglish().ConvertShamsiToMiladi();
            bill.UserId = userId;

            var res = _creditService.AddCreditBill(bill);

            if (res)
            {
                TempData["success"] =
                    "فیش واریزی شما با موفقیت در سیستم ثبت شد,  کارشناسان ما پس از بررسی فیش واریزی شما را تایید میکنند";
                return RedirectToAction("ListCreditBill");
            }

            return View();
        }
     

        #endregion

        #region Validation

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPostcode(string codePostiNeshaniMahaleKar)
        {


            //if (_addressService.CheckBlockPostalCode(codePostiNeshaniMahaleKar))
            //    return Json("لطفا کد پستی صحیح وارد کنید");



            return Json(true);

        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyNationalNumber(string nationalNumber)
        {


            if (!nationalNumber.IsValidNationalCode())
                return Json("لطفا شماره ی ملی صحیح وارد کنید");



            return Json(true);

        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyBDate(string BDate)
        {

            if (!BDate.IsValidBDate())
                return Json("لطفاً تاریخ تولد صحیح را وارد نمایید");



            return Json(true);

        }

        #endregion



    }
}
