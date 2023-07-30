using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EShop.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly IFavoriteService _favoriteService;
        private readonly IOrderService _orderService;
        private readonly ICommentService _commentService; private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        public ProfileController(IAddressService addressService, IUserService userService, IFavoriteService favoriteService, IOrderService orderService, ICommentService commentService, IConfiguration config, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _addressService = addressService;
            _userService = userService;
            _favoriteService = favoriteService;
            _orderService = orderService;
            _commentService = commentService;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst("userid").Value;
            var user = _userService.GetUserById(Convert.ToInt64(userId));
            if (user.TypeUser == EnumTypeUser.Admin)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            return View(user);
        }

        #region EditProfile

        public IActionResult EditProfile()
        {
            var userId = User.FindFirst("userid").Value;
            ViewBag.provincelist = _addressService.GetProvince();

            var user = _userService.GetUserForUpdate(Convert.ToInt64(userId));
            if (user.Name == null && user.Family == null && user.FullName != null)
            {
                var namefamily = user.FullName?.Split(' ', 2);
                if (namefamily != null)
                {
                    user.Name = namefamily[0];
                    if (namefamily.Length > 1)
                        user.Family = namefamily[1];

                }
            }
            return View("EditProfile", user);
        }
        [HttpPost]
        [SanitizeInput]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditProfile(UserEditProfile user)
        {
            ViewBag.provincelist = _addressService.GetProvince();

            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //        _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(user);
            //    }
            //}

            if (_addressService.CheckBlockPostalCode(user.PostalCode))
                ModelState.AddModelError("PostalCode", "لطفا کد پستی صحیح وارد کنید");
            if (!user.NatioalCode.IsValidNationalCode())
                ModelState.AddModelError("NatioalCode", "لطفا کد ملی صحیح وارد کنید");

            if (user.IsHoghughi)
            {
                if (string.IsNullOrWhiteSpace(user.CompanyName))
                    ModelState.AddModelError("CompanyName", "لطفا نام شرکت صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.CodeEghtesadi))
                    ModelState.AddModelError("CodeEghtesadi", "لطفا کد اقتصادی شرکت صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.ShomareSabt))
                    ModelState.AddModelError("ShomareSabt", "لطفا شماره ی ثبت شرکت صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.CodeNaghshTajer))
                    ModelState.AddModelError("CodeNaghshTajer", "لطفا کد نقش تاجر صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.TarikhTasis))
                    ModelState.AddModelError("TarikhTasis", "لطفا تاریخ تاسیس شرکت صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.ShenaseMeli))
                    ModelState.AddModelError("ShenaseMeli", "لطفا شناسه ی ملی شرکت صحیح وارد کنید");

                if (user.NoeSherkat == null)
                    ModelState.AddModelError("NoeSherkat", "لطفا نوع شرکت صحیح وارد کنید");

                if (user.NoeMalekiyat == null)
                    ModelState.AddModelError("NoeMalekiyat", "لطفا نوع مالکیت صحیح وارد کنید");

                if (user.NoeTamalok == null)
                    ModelState.AddModelError("NoeTamalok", "لطفا نوع تملک محل فعالیت صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.NeshaniMahaleKar))
                    ModelState.AddModelError("NeshaniMahaleKar", "لطفا نشانی محل کار صحیح وارد کنید");

                if (string.IsNullOrWhiteSpace(user.CodePostiNeshaniMahaleKar))
                    ModelState.AddModelError("CodePostiNeshaniMahaleKar", "لطفا کد پستی صحیح وارد کنید");

                //if (user.FormFileRuznameRasmi == null && string.IsNullOrWhiteSpace(user.FileRuznameRasmi))
                //    ModelState.AddModelError("FormFileRuznameRasmi", "لطفا روزنامه رسمی را بارگذاری کنید");

                if (user.FormFileAkharinTaghirat == null && string.IsNullOrWhiteSpace(user.FileAkharinTaghirat))
                    ModelState.AddModelError("FormFileAkharinTaghirat", "لطفا گواهی آخرین تغییرات را بارگذاری کنید");

                if (user.FormFileSahebanEmza == null && string.IsNullOrWhiteSpace(user.FileSahebanEmza))
                    ModelState.AddModelError("FormFileSahebanEmza", "لطفا آگهی تاسیس شرکت ها را بارگذاری کنید");

                if (user.FormFileAgahiTasis == null && string.IsNullOrWhiteSpace(user.FileAgahiTasis))
                    ModelState.AddModelError("FormFileAgahiTasis", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");
                //
                //if (user.FormFileRuznameRasmi != null)
                //    if (!ImageSecurity.Imagevalidator(user.FormFileRuznameRasmi))
                //        ModelState.AddModelError($"FormFileRuznameRasmi", "لطفا روزنامه رسمی را بارگذاری کنید");
                if (user.FormFileAkharinTaghirat != null)
                    if (!ImageSecurity.Imagevalidator(user.FormFileAkharinTaghirat))
                        ModelState.AddModelError($"FormFileAkharinTaghirat", "لطفا گواهی آخرین تغییرات را بارگذاری کنید");
                if (user.FormFileSahebanEmza != null)
                    if (!ImageSecurity.Imagevalidator(user.FormFileSahebanEmza))
                        ModelState.AddModelError($"FormFileSahebanEmza", "لطفا آگهی تاسیس شرکت ها را بارگذاری کنید");
                if (user.FormFileAgahiTasis != null)
                    if (!ImageSecurity.Imagevalidator(user.FormFileAgahiTasis))
                        ModelState.AddModelError($"FormFileAgahiTasis", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");




            }



            if (!ModelState.IsValid)
                return View(user);

            //

            if (user.IsHoghughi)
            {
                //if (user.FormFileRuznameRasmi != null)
                //    user.FileRuznameRasmi = user.FormFileRuznameRasmi.SaveImage("", "wwwroot/uploads",false);

                if (user.FormFileAkharinTaghirat != null)
                    user.FileAkharinTaghirat = user.FormFileAkharinTaghirat.SaveImage("", "wwwroot/uploads", false);

                if (user.FormFileSahebanEmza != null)
                    user.FileSahebanEmza = user.FormFileSahebanEmza.SaveImage("", "wwwroot/uploads", false);

                if (user.FormFileAgahiTasis != null)
                    user.FileAgahiTasis = user.FormFileAgahiTasis.SaveImage("", "wwwroot/uploads", false);

            }
            //



            var res = _userService.EditUserProfile(user);

            //
            if (user.IsHoghughi)
            {
                if (user.UserLegalId == null)
                {
                    var userLegal = _mapper.Map<UserLegal>(user);

                    userLegal.UserId = user.Id;
                    userLegal.Id = 0;
                    _userService.AddUserLegal(userLegal);
                }
                else
                {
                    var userLegal = _mapper.Map<UserLegal>(user);

                    userLegal.Id = user.UserLegalId.Value;
                    userLegal.UserId = user.Id;
                    _userService.EditUserLegal(userLegal);
                }

            }
            else
            {
                if (user.UserLegalId != null)
                {

                    _userService.DeleteUserLegalById(user.UserLegalId.Value);
                }
            }

            //



            if (res)
                TempData["SuccessMessage"] = "تغییرات با موفقیت ثبت شد";
            else
                TempData["ErrorMessage"] = "خطا در ثبت اطلاعات! لطفا دوباره تلاش کنید";
            return RedirectToAction("Index");
        }
        #endregion

        #region Address
        public IActionResult Addresses()
        {
            var userid = User.FindFirst("userid").Value;
            ViewBag.provincelist = _addressService.GetProvince();
            AddressListViewModel model = new AddressListViewModel
            {
                AddressList = _addressService.GetUserAddresses(int.Parse(userid))
            };
            return View(model);
        }

        public JsonResult AjaxAddresses()
        {
            var userid = User.FindFirst("userid").Value;
            ViewBag.provincelist = _addressService.GetProvince();
            AddressListViewModel model = new AddressListViewModel
            {
                AddressList = _addressService.GetUserAddresses(int.Parse(userid))
            };
            return Json(JsonConvert.SerializeObject(model));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> AjaxCreateAddress([FromBody] AddressViewModel model)
        {

            //if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //    _config["GoogleReCaptcha:secret"]))
            //{
            //    ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //    return View(model);
            //}

            var userid = User.FindFirst("userid").Value;
            AddressListViewModel addresses = new AddressListViewModel();

            if (ModelState.IsValid)
            {
                UserAddress address = new UserAddress
                {
                    CityId = model.CityId,
                    ProvinceId = model.ProvinceId,
                    FullName = model.FullName,
                    PostalCode = model.PostalCode,
                    PostalAddress = model.PostalAddress,
                    Phone = model.Phone,
                    UserId = int.Parse(userid)


                };
                if (_addressService.AddAddress(address))
                {
                    var res = _addressService.ChangeDefaultUserAddress(Convert.ToInt64(userid), address.Id);

                    ViewBag.provincelist = _addressService.GetProvince();
                    addresses = new AddressListViewModel
                    {
                        AddressList = _addressService.GetUserAddresses(int.Parse(userid))
                    };
                    return Json(JsonConvert.SerializeObject(addresses));
                }
            }

            return Json(JsonConvert.SerializeObject(addresses));
        }

        public IActionResult AddressContent()
        {
            var userid = User.FindFirst("userid").Value;
            AddressListViewModel model = new AddressListViewModel
            {
                AddressList = _addressService.GetUserAddresses(int.Parse(userid))
            };
            return View(model);
        }

        public IActionResult ChangeDefaultUserAddress(long id)
        {
            var userid = User.FindFirst("userid").Value;
            var res = _addressService.ChangeDefaultUserAddress(Convert.ToInt64(userid), id);
            return RedirectToAction("Addresses");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public IActionResult GetCity(int id)
        {
            return Json(_addressService.GetCityByProvinceId(id));
        }

        public IActionResult CreateAddress()
        {
            var isColleague = _userService.IsUserColleague();

            ViewBag.isColleague = isColleague;

            ViewBag.provincelist = _addressService.GetProvince();

            return View();
        }

        [HttpPost]
        [SanitizeInput]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateAddress(AddressListViewModel model)
        {
            var isColleague = _userService.IsUserColleague();
            ViewBag.provincelist = _addressService.GetProvince();
            ViewBag.isColleague = isColleague;

            if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
                _config["GoogleReCaptcha:secret"]))
            {
                ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
                return View(model);
            }

            if (_addressService.CheckBlockPostalCode(model.Address.PostalCode))
            {
                ViewBag.Error = "لطفا کد پستی صحیح وارد کنید";
                return View(model);
            }
            var userid = User.FindFirst("userid").Value;

            if (isColleague)
            {
                var user = _userService.GetUserById(Convert.ToInt64(userid));
                model.Address.FullName = user.FullName;
                model.Address.Phone = user.Phone;
                ModelState.Remove("Address.FullName");
                ModelState.Remove("Address.Phone");

            }

            if (ModelState.IsValid)
            {
                UserAddress address = new UserAddress
                {
                    CityId = model.Address.CityId,
                    ProvinceId = model.Address.ProvinceId,
                    FullName = model.Address.FullName,
                    PostalCode = model.Address.PostalCode,
                    PostalAddress = model.Address.PostalAddress,
                    Phone = model.Address.Phone,
                    UserId = int.Parse(userid)

                };
                if (_addressService.AddAddress(address))
                    return View("Addresses");
            }
            ViewBag.provincelist = _addressService.GetProvince();

            ViewBag.Error = "خطا در ثبت اطلاعات";
            return View(model);
        }
        #endregion


        #region Favorite
        public IActionResult FavoriteList()
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            UserProductFovorites userProductFovorites = new UserProductFovorites();

            return View(_favoriteService.GetUserProductFavorites(userid));
        }

        public IActionResult FavoriteListContent()
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            return View(_favoriteService.GetUserProductFavorites(userid));
        }

        public IActionResult RemoveFavorite(long id)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            if (_favoriteService.CheckEsxistProductFavoriteForUser(userid, id))
            {
                UserProductFovorites model = new UserProductFovorites { Id = id };
                if (_favoriteService.RemoveUserProductFavorite(model))
                {
                    return RedirectToAction("FavoriteList");
                }
            }
            return RedirectToAction("FavoriteList");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ChangeFavoriteState(long productid)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            long favoriteid = _favoriteService.GetFavoriteId(userid, productid);
            UserProductFovorites userProductFovorites = new UserProductFovorites();
            if (favoriteid > 0)
            {
                userProductFovorites.Id = favoriteid;
                if (_favoriteService.RemoveUserProductFavorite(userProductFovorites))
                    return Json(1);
            }
            else
            {
                userProductFovorites.UserId = userid;
                userProductFovorites.ProductId = productid;
                if (_favoriteService.AddProductFavorite(userProductFovorites))
                    return Json(2);
            }
            return Json(3);
        }

        [HttpPost]
        public IActionResult CheckFavoriteisExist(int productid)
        {
            var userid = int.Parse(User.FindFirst("userid").Value);
            return Json(_favoriteService.CheckEsxistProductFavoriteForUserByProductId(userid, productid));
        }
        #endregion

        #region  Orders

        public IActionResult Orders()
        {
            var userid = User.FindFirst("userid").Value;
            var res = _orderService.GetListUserOrder(Convert.ToInt64(userid));
            return View("Orders", res);
        }
        public IActionResult PrintOrder(long id)
        {
            var userid = User.FindFirst("userid").Value;

            var res = _orderService.GetOrderForPrint(id, Convert.ToInt64(userid));
            if (res == null)
                return RedirectToAction("Index");
            return View(res);
        }

        #endregion


        #region Comment
        [HttpPost]
        [SanitizeInput]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserComment(UserCommentVM commentVm)
        {
            //if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //    _config["GoogleReCaptcha:secret"]))
            //{
            //    ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //    return RedirectToAction("Index", "Product", new { area = "", id = commentVm.ProductId });
            //}

            string userId = null;
            try
            {
                userId = User.FindFirst("userid").Value;
            }
            catch (Exception e)
            {

            }

            var comment = new Comment
            {
                CommentDisLike = 0,
                CommentLike = 0,
                CommentTitle = commentVm.CommentTitle,
                Name = commentVm.Name,
                Mobile = commentVm.Mobile,
                CommentText = commentVm.CommentText,
                StatusComment = EnumStatusComment.Wait,
                Negative = commentVm.Negative,
                Positive = commentVm.Positive,
                ProductId = commentVm.ProductId,
                UserId = userId != null ? Convert.ToInt64(userId) : null,
                TypeSystem = EnumTypeSystem.Farnaa


            };

            var res = _commentService.AddComment(comment);

            return RedirectToAction("Index", "Product", new { area = "", id = commentVm.ProductId });
        }

        public IActionResult Comments()
        {
            var userId = User.FindFirst("userid").Value;


            var res = _commentService.GetUserComments(Convert.ToInt64(userId));
            return View(res);
        }

        #endregion


    }
}