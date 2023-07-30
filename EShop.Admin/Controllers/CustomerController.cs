using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class CustomerController : BaseAdminController
    {
        private IUserService _userService;
        private ICommentService _commentService;

        public CustomerController(IUserService userService, ICommentService commentService,
            Logging.AuditLog.IAuditService logger,IHttpContextAccessor contextAccessor) 
            : base(logger,contextAccessor)
        {
            _userService = userService;
            _commentService = commentService;
        }


        #region Customer

       

        #region EditColleauge

        public IActionResult DeletePro(long id)
        {
            _userService.DeleteUserById(id);

            return RedirectToAction("IndexColleauePro");

        }
        public IActionResult ChangeUserStatus(long id,bool status)
        {
            _userService.ChangeUserStatus(id, status);

            return RedirectToAction("IndexColleauePro");

        }
        [HttpPost]
        public IActionResult IncreaseCreditPro(IncreaseCreditUserColleaugeViewModel data)
        {
            var res = _userService.IncreaseCreditPro(data.Id, data.Price);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("IndexColleauePro");

        }


        [HttpPost]
        public IActionResult EditPro(DataLayer.Entities.User.User data)
        {
            var user = _userService.GetUserById(data.Id);
            user.Name = data.Name;
            user.Family = data.Family;
            user.FullName = data.FullName;
            user.Email = data.Email;
            user.Phone = data.Phone;
            user.Username = data.Username;
            user.CardNumber = data.CardNumber;
            var res = _userService.EditUser(user);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("IndexColleauePro");

        }
        public IActionResult IndexColleaue()
        {
            return View();

        }
        public IActionResult IndexColleauePro()
        {
            return View();

        }

        public IActionResult IncreaseCredit(long id)
        {
            var user = _userService.GetById(id);
            IncreaseCreditUserColleaugeViewModel data = new IncreaseCreditUserColleaugeViewModel
            {
                Id = id,
                Name = user.FullName,
                Phone = user.Phone,
                Price = 0,
                Username = user.Username
            };
            return View(data);

        }
        [HttpPost]
        public IActionResult IncreaseCredit(IncreaseCreditUserColleaugeViewModel data)
        {
            var res = _userService.IncreaseCredit(data.Id, data.Price);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("IndexColleaue");

        }
        public IActionResult IncreaseCreditPro(long id)
        {
            var user = _userService.GetUserById(id);
            IncreaseCreditUserColleaugeViewModel data = new IncreaseCreditUserColleaugeViewModel
            {
                Id = id,
                Name = user.FullName,
                Phone = user.Phone,
                Price = user.AcceptPrice != null ? user.AcceptPrice.Value : 0,
                Username = user.Username
            };
            return View(data);

        }
        public IActionResult EditPro(long id)
        {
            var user = _userService.GetUserById(id);

            return View(user);

        }


        public IActionResult ListColleaue(UserColleagueSerchViewModel search)
        {


            var content = _userService.GetListColleauge(search, 25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;

            return View("_ListColleaue", content.Item2);

        }
        public IActionResult ListColleauePro(UserColleagueSerchViewModel search)
        {


            var content = _userService.GetListColleauge(search, 25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;

            return View("_ListColleauePro", content.Item2);

        }
      
        #endregion

        #region EditUser

        public IActionResult EditCustomer(long id)
        {
            var user = _userService.GetUserById(id);

            return View(user);

        }
        [HttpPost]
        public IActionResult EditCustomer(DataLayer.Entities.User.User data)
        {
            var user = _userService.GetUserById(data.Id);
            user.Name = data.Name;
            user.Family = data.Family;
            user.FullName = data.FullName;
            user.Email = data.Email;
            user.Phone = data.Phone;
             var res = _userService.EditUser(user);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("IndexCustomerForEditContainer");

        }
        public IActionResult IndexCustomerForEditContainer()
        {
            return View();

        }
        public IActionResult ListCustomerForEdit(UserCustomerSerchViewModel search)
        {


            var content = _userService.GetListCustomer(search, 25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;

            return View("_ListCustomerForEdit", content.Item2);

        }

        #endregion

#region EditAddress

        public IActionResult EditAddress(long id)
        {
            var res = _userService.GetUserAddressById(id);

            return View(res);

        }
        [HttpPost]
        public IActionResult EditAddress(UserAddress data)
        {
            var address = _userService.GetUserAddressById(data.Id);
            address.Name = data.Name;
            address.Family = data.Family;
            address.FullName = data.FullName;
            address.Phone = data.Phone;
            address.PostalAddress = data.PostalAddress;
            address.PostalCode = data.PostalCode;
                       var res = _userService.EditUserAddress(address);
            TempData["res"] = res ? "success" : "faild";

            return RedirectToAction("IndexCustomerAddressForEdit");

        }
        public IActionResult IndexCustomerAddressForEdit()
        {
            return View();

        }
        public IActionResult ListCustomerAddressForEdit(UserAddressSerchViewModel search)
        {


            var content = _userService.GetListUserAddress(search, 25);
            ViewBag.count = content.Item1;

            ViewBag.PageNumber = search.Pagenumber;

            return View("_ListCustomerAddressForEdit", content.Item2);

        }

        #endregion

        public IActionResult Index()
        {
             var res = _userService.GetCustomerForAdmin();
            return View( res);
        }

         public ActionResult CustomerAddress()
         {
             return View();
         }
         public ActionResult Customers()
         {
             return View();
         }


         [HttpGet]
         public object GetCustomers(DataSourceLoadOptions loadOptions)
         {
             var res = _userService.GetCustomers();
             return DataSourceLoader.Load(res, loadOptions); ;
         }

         [HttpGet]
         public object GetCustomerAddress(DataSourceLoadOptions loadOptions)
         {
             var res = _userService.GetCustomerAddress();
             return DataSourceLoader.Load(res, loadOptions); ;
         }
        #endregion


        #region Comments

        public IActionResult UserComments()
        {
            var res = _commentService.GetUserCommentsForAdmin();
            return View(res);

        }

        public IActionResult ChangeUserCommentStatus(long id,EnumStatusComment statusComment)
        {
            TempData["res"] = "faild";
            if (_commentService.ChangeUserCommentStatus(id, statusComment))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = id,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("UserComments");
        }

        #endregion



    }
}