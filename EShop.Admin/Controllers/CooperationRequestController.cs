using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Sender;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class CooperationRequestController : BaseAdminController
    {
        readonly ICooperationRequestService _cooperationRequestService;
        private readonly ISmsSender _smsSender;
        private IMapper _mapper;
        private IAddressService _addressService;
        private IUserService _userService;

        public CooperationRequestController(ICooperationRequestService cooperationRequestService, ISmsSender smsSender, IMapper mapper, IAddressService addressService, IUserService userService)
        {
            _cooperationRequestService = cooperationRequestService;
            _smsSender = smsSender;
            _mapper = mapper;
            _addressService = addressService;
            _userService = userService;
        }


        public ActionResult Index(EnumCooperationRequestStatus Status)
        {
            ViewBag.Status = Status;
             return View();
        }

        public IActionResult List(ColleagueSearchViewModel search)
        {
            if (search.Pagenumber == 0)
                search.Pagenumber = 1;
            var content = _cooperationRequestService.GetListForAdmin(search,25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;
            ViewBag.Status = search.Status;
            ViewBag.Name = search.Name;
            ViewBag.Code = search.Code;

            return View("_List", content.Item2);
        }


        public IActionResult Detail(int id, EnumCooperationRequestStatus Status)
        {
            TempData["Status"] = Status;

            var data = _cooperationRequestService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.provincelist = _addressService.GetProvince();
            ViewBag.Citylist = _addressService.GetCity();

            if (data.Type == EnumRealLegal.Real)
            {
                var res = _mapper.Map<CooperationRequestRealViewModel>(data);
         
                return View("DetailReal", res);

            }
            else
            {
                var res = _mapper.Map<CooperationRequestLegalViewModel>(data);
 
                return View("DetailLegal", res);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditStatus(long id, string Description, EnumCooperationRequestStatus Status)
        {
            var res = _cooperationRequestService.ChangeStatus(id, Description, Status);

            if (Status == EnumCooperationRequestStatus.Confairm)
            {
                var pass = CodeGenerator.PasswordGenerator();
                var hashPass = string.Join("-", PasswordHash.HashPasswordV2(pass));
                var phone = _cooperationRequestService.FindById(id).ShomareTamas;
                res = _cooperationRequestService.ConvertRequestToUser(id, hashPass);
                if(res)
                await _smsSender.SendSms(phone, "ConfirmRequest", pass);

            }


            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("Index");
        }

 
        public async Task<IActionResult> ResetPassword(long id)
        {
            var request = _cooperationRequestService.FindById(id);

            if (request.Status == EnumCooperationRequestStatus.Confairm)
            {
                var pass = CodeGenerator.PasswordGenerator();
                var hashPass = string.Join("-", PasswordHash.HashPasswordV2(pass));
                var user = _userService.GetUserByUsername(request.CodeMeli);
                var res = _userService.ResetPassword(user.Id, hashPass);
                if(res)
                await _smsSender.SendSms(user.Phone, "ConfirmRequest", pass);

                TempData["res"] = res ? "success" : "faild";


            }



            return RedirectToAction("Index");
        }


    }
}