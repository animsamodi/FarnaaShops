using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Sender;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace EShop.Web.Controllers
{
    [Route("users")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IRenderViewToString _renderViewToString;
        private readonly ISmsSender _smsSender;
        private readonly ICartService _cartService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private ICooperationRequestService _cooperationRequestService;

        public AccountController(IUserService userService, IConfiguration config,
            IRenderViewToString renderViewToString, ISmsSender smsSender, ICartService cartService, IHostingEnvironment hostingEnvironment, IAddressService addressService, IMapper mapper, ICooperationRequestService cooperationRequestService)
        {
            _userService = userService;
            _config = config;
            _renderViewToString = renderViewToString;
            _smsSender = smsSender;
            _cartService = cartService;
            _hostingEnvironment = hostingEnvironment;
            _addressService = addressService;
            _mapper = mapper;
            _cooperationRequestService = cooperationRequestService;
        }

        #region MyRegion

        [Route("CheckUserAuthenticated")]
        public IActionResult CheckUserAuthenticated()
        {
            return Json(User.Identity.IsAuthenticated);
        }



        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region  password
        #endregion

        #region Register


        [Route("RegisterColleagueReal")]
        public IActionResult RegisterColleagueReal()
        {
            ViewBag.provincelist = _addressService.GetProvince();

            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }
            return View();
        }
        [HttpPost]
        [Route("RegisterColleagueReal")]
        public async Task<IActionResult> RegisterColleagueReal(CooperationRequestRealViewModel model)
        {
            ViewBag.provincelist = _addressService.GetProvince();

            model.Type = EnumRealLegal.Real;
            model.Status = EnumCooperationRequestStatus.Wating;

            TempData["UserId"] = JsonConvert.SerializeObject(model.Id);
            TempData["Phone"] = JsonConvert.SerializeObject(model.ShomareTamas);

            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //        _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            if (model.FormFileKartMeli == null)
                ModelState.AddModelError("FileKartMeli", "لطفا کپی کارت ملی صاحب جواز کسب را بارگذاری کنید");

            if (model.FormFileParvaneKasb == null)
                ModelState.AddModelError("FileParvaneKasb", "لطفا کپی پروانه کسب را بارگذاری کنید");

            //if (model.FormFileSanad == null)
            //    ModelState.AddModelError("FileSanad", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");

            //
            if (model.FormFileKartMeli != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileKartMeli))
                    ModelState.AddModelError($"FileKartMeli", "لطفا کپی کارت ملی صاحب جواز کسب را بارگذاری کنید");
            if (model.FormFileParvaneKasb != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileParvaneKasb))
                    ModelState.AddModelError($"FileParvaneKasb", "لطفا کپی پروانه کسب را بارگذاری کنید");
            if (model.FormFileSanad != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileSanad))
                    ModelState.AddModelError($"FileSahebanEmza", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");


            //

            if (!ModelState.IsValid)
                return View(model);

            var entity = _mapper.Map<CooperationRequest>(model);




            entity.FileKartMeli = model.FormFileKartMeli.SaveImage("", "wwwroot/uploads", false);
            entity.FileParvaneKasb = model.FormFileParvaneKasb.SaveImage("", "wwwroot/uploads", false);
            if (model.FormFileSanad != null)
                entity.FileSanad = model.FormFileSanad.SaveImage("", "wwwroot/uploads", false);

            entity.Id = model.Id;
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
            //var res = _cooperationRequestService.Add(entity);
            var res = _cooperationRequestService.Update(entity);
            if (res)
            {
                TempData["success"] =
                    "همکار ارجمند فرنا، درخواست شما در پلتفرم فرنا، با موفقیت ثبت گردید. منتظر پاسخ همکاران باشید.";
                return View();
            }

            return View(model);
        }
        [Route("RegisterColleagueLegal")]
        public IActionResult RegisterColleagueLegal()
        {
            ViewBag.provincelist = _addressService.GetProvince();

            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }
            return View();
        }
        [HttpPost]
        [Route("RegisterColleagueLegal")]
        public async Task<IActionResult> RegisterColleagueLegal(CooperationRequestLegalViewModel model)
        {
            ViewBag.provincelist = _addressService.GetProvince();

            model.Type = EnumRealLegal.Legal;
            model.Status = EnumCooperationRequestStatus.Wating;

            TempData["UserId"] = JsonConvert.SerializeObject(model.Id);
            TempData["Phone"] = JsonConvert.SerializeObject(model.ShomareTamas);

            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //        _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}



            if (model.FormFileKartMeli == null)
                ModelState.AddModelError("FileKartMeli", "لطفا آگهی تاسیس شرکت ها را بارگذاری کنید");

            if (model.FormFileParvaneKasb == null)
                ModelState.AddModelError("FileParvaneKasb", "لطفا آگهی آخرین روزنامه را بارگذاری کنید");

            if (model.FormFileSanad == null)
                ModelState.AddModelError("FileSanad", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");

            //

            if (model.FormFileKartMeli != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileKartMeli))
                    ModelState.AddModelError($"FileKartMeli", "لطفا آگهی تاسیس شرکت ها را بارگذاری کنید");
            if (model.FormFileParvaneKasb != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileParvaneKasb))
                    ModelState.AddModelError($"FileParvaneKasb", "لطفا آگهی آخرین روزنامه را بارگذاری کنید");
            if (model.FormFileSanad != null)
                if (!ImageSecurity.Imagevalidator(model.FormFileSanad))
                    ModelState.AddModelError($"FileSahebanEmza", "لطفا کپی سند مالکیت یا اجاره نامه رسمی را بارگذاری کنید");


            //

            if (!ModelState.IsValid)
                return View(model);

            var entity = _mapper.Map<CooperationRequest>(model);




            entity.FileKartMeli = model.FormFileKartMeli.SaveImage("", "wwwroot/uploads", false);
            entity.FileParvaneKasb = model.FormFileParvaneKasb.SaveImage("", "wwwroot/uploads", false);
            entity.FileSanad = model.FormFileSanad.SaveImage("", "wwwroot/uploads", false);




            entity.Id = model.Id;
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
             //var res = _cooperationRequestService.Add(entity);
            var res = _cooperationRequestService.Update(entity);


             if (res)
            {
                TempData["success"] =
                    "همکار ارجمند فرنا، درخواست شما در پلتفرم فرنا، با موفقیت ثبت گردید. منتظر پاسخ همکاران باشید.";
                return View();
            }

            return View(model);
        }
        [Route("LoginColleague")]
        public IActionResult LoginColleague()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        [Route("LoginColleague")]
        [ValidateAntiForgeryToken]
        [SanitizeInput]

        public async Task<IActionResult> LoginColleague(LoginColleagueViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //             _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            var existUsername = _userService.CheckExistUsername(model.Username);
            if (existUsername != null)
            {
                //Check Password
                //ModelState.AddModelError(string.Empty, "نام کاربری و یا کلمه عبور اشتباه است");
                // return View(model);
                //
                if (existUsername.TypeUser != EnumTypeUser.User || !existUsername.IsColleague)
                {
                    ModelState.AddModelError("", "کاربر گرامی! شما دسترسی برای ورود را ندارید");
                    return View(model);

                }

                if (existUsername.IsActive)
                {
                    string[] tempstring = existUsername.Password.Split("-");
                    byte[] hashpassword = new byte[tempstring.Length];
                    for (int i = 0; i < tempstring.Length; i++)
                        hashpassword[i] = Convert.ToByte(tempstring[i]);
                    if (PasswordHash.VerifyHashedPasswordV2(hashpassword, model.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("userid",existUsername.Id.ToString()),
                            new Claim("name",String.IsNullOrEmpty(existUsername.FullName) ? "کاربر جدید" : existUsername.FullName),
                            new Claim("phone", existUsername.Phone ),
                            new Claim("date", existUsername.CreateDate.GetMonthPersian() ),
                            new Claim("userName", existUsername.Username),
                            new Claim("avatar",existUsername.Avatar.ToString()),
                            new Claim("IsColleauge",existUsername.IsColleague.ToString())
                        };

                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = false
                        };
                        HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")), properties);
                        ChangeCartCookieToUser(existUsername.Id);

                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "کاربری با این مشخصات پیدا نشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "حساب کابری شما فعال نیست");
                    return View(model);
                }


            }




            ModelState.AddModelError(string.Empty, "نام کاربری و یا کلمه عبور اشتباه است");
            return View(model);
        }

        [Route("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        [SanitizeInput]

        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //             _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            var existPhone = _userService.CheckExistPhone(model.Phone);
            if (existPhone != null)
            {
                if (existPhone.TypeUser != EnumTypeUser.User)
                {
                    ModelState.AddModelError("", "کاربر گرامی! شما  دسترسی برای ورود را ندارید");
                    return View(model);

                }
                if (!existPhone.IsActive)
                {
                    ModelState.AddModelError("", "حساب کابری شما فعال نیست"); return View(model);
                }
                if (existPhone.IsColleague)
                {
                    ModelState.AddModelError("", "همکار گرامی لطفا از بخش همکاران وارد پنل خود شوید"); return View(model);
                }


            }
            string activecode = Guid.NewGuid().ToString().Replace("-", "");

            #region ActiveByEmail
            #endregion

            int phoneactivecode =/* Convert.ToInt32(model.Phone.Substring(7, 4)); */CodeGenerator.PhoneActiveCodeGenerator();
            if (existPhone != null)
            {
              //  if (!string.IsNullOrEmpty(existPhone.NatioalCode))
                //    phoneactivecode = Convert.ToInt32(existPhone.NatioalCode.Substring(6, 4));
                if (existPhone.PhoneActiveCodeExpDate > DateTime.Now)
                {

                    TempData["UserId"] = JsonConvert.SerializeObject(existPhone.Id);
                    if (_hostingEnvironment.IsDevelopment())
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                    }
                    else
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                    }

                    return RedirectToAction("ConfirmPhone");

                }
                else
                {
                    existPhone.PhoneActiveCode = phoneactivecode;
                    existPhone.PhoneActiveCodeExpDate = DateTime.Now.AddMinutes(2);

                    if (_userService.EditUser(existPhone))
                    {
                        await _smsSender.SendSms(model.Phone, "verify", phoneactivecode.ToString());


                        TempData["UserId"] = JsonConvert.SerializeObject(existPhone.Id);
                        if (_hostingEnvironment.IsDevelopment())
                        {
                            TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                        }
                        else
                        {
                            TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                        }
                        return RedirectToAction("ConfirmPhone");
                    }
                }

            }
            else
            {
                User user = new DataLayer.Entities.User.User
                {
                    Phone = model.Phone,
                    EmailActiveCode = activecode,
                    PhoneActiveCodeExpDate = DateTime.Now.AddMinutes(2),
                    PhoneActiveCode = phoneactivecode,
                    TypeUser = EnumTypeUser.User,
                    IsActive = true,
                    RoleId = 1,
                    TypeSystem = EnumTypeSystem.Farnaa
                };
                long userid = _userService.AddUser(user);

                if (userid > 0)
                {
                    await _smsSender.SendSms(model.Phone, "verify", phoneactivecode.ToString());


                    TempData["UserId"] = JsonConvert.SerializeObject(userid);
                    if (_hostingEnvironment.IsDevelopment())
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                    }
                    else
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                    }
                    return RedirectToAction("ConfirmPhone");
                }
            }


            ModelState.AddModelError(string.Empty, "در ثبت اطلاعات مشکلی بوجود آمده است");
            return View(model);
        }


        [Route("RegisterColleague")]
        public IActionResult RegisterColleague(EnumRealLegal type)
        {
            TempData["Type"] = type;
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        [Route("RegisterColleague")]
        [ValidateAntiForgeryToken]
        [SanitizeInput]

        public async Task<IActionResult> RegisterColleague(RegisterUserViewModel model)
        {

            TempData["Type"] = model.Type;
            if (!ModelState.IsValid)
                return View(model);


            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //             _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            var existPhone = _cooperationRequestService.CheckExistPhoneInRequest(model.Phone);
            if (existPhone != null)
            {
                if (existPhone.Status == EnumCooperationRequestStatus.Confairm)
                {
                    ModelState.AddModelError("", "همکار گرامی لطفا از بخش همکاران وارد پنل خود شوید");
                    return View(model);

                }
                if (existPhone.Status == EnumCooperationRequestStatus.Wating)
                {
                    ModelState.AddModelError("", "همکار گرامی درخواست همکاری شما در حال بررسی میباشد.نتیجه ی درخواست از طریق پیامک به شما اعلام میشود"); return View(model);
                    return View(model);

                }


            }
            string activecode = Guid.NewGuid().ToString().Replace("-", "");


            int phoneactivecode = CodeGenerator.PhoneActiveCodeGenerator();
            if (existPhone != null && existPhone.Status != EnumCooperationRequestStatus.Reject)
            {
                if (existPhone.PhoneActiveCodeExpDate > DateTime.Now)
                {

                    TempData["UserId"] = JsonConvert.SerializeObject(existPhone.Id);
                    if (_hostingEnvironment.IsDevelopment())
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                    }
                    else
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                    }

                    return View("ConfirmPhoneColleague");

                }
                else
                {
                    existPhone.PhoneActiveCode = phoneactivecode;
                    existPhone.PhoneActiveCodeExpDate = DateTime.Now.AddMinutes(2);

                    if (_cooperationRequestService.EditRequest(existPhone))
                    {
                        await _smsSender.SendSms(model.Phone, "verify", phoneactivecode.ToString());


                        TempData["UserId"] = JsonConvert.SerializeObject(existPhone.Id);
                        if (_hostingEnvironment.IsDevelopment())
                        {
                            TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                        }
                        else
                        {
                            TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                        }
                        return View("ConfirmPhoneColleague");
                    }
                }

            }
            else
            {
                CooperationRequest request = new CooperationRequest
                {
                    ShomareTamas = model.Phone,
                    Type = model.Type,
                    PhoneActiveCodeExpDate = DateTime.Now.AddMinutes(2),
                    PhoneActiveCode = phoneactivecode,
                    IsDelete = false,
                    IsConfirmMobile = false,
                    CityId = 1,
                    ProvinceId = 1,
                    //TODO Status = EnumCooperationRequestStatus.WatingConfirmPhone
                    Status = EnumCooperationRequestStatus.WatingConfirmPhone,
                    TypeSystem = EnumTypeSystem.Farnaa
                };
                long id = _cooperationRequestService.AddRequest(request);

                if (id > 0)
                {
                    await _smsSender.SendSms(model.Phone, "verify", phoneactivecode.ToString());


                    TempData["UserId"] = JsonConvert.SerializeObject(id);
                    if (_hostingEnvironment.IsDevelopment())
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone + " - " + phoneactivecode);

                    }
                    else
                    {
                        TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);

                    }
                    return View("ConfirmPhoneColleague");
                }
            }


            ModelState.AddModelError(string.Empty, "در ثبت اطلاعات مشکلی بوجود آمده است");
            return View(model);
        }
        [Route("ConfirmPhone")]
        public IActionResult ConfirmPhone()
        {
            return View();
        }
        [Route("ConfirmPhone")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SanitizeInput]

        public async Task<IActionResult> ConfirmPhone(ActivePhoneViewModel model)
        {
            //if (!ModelState.IsValid)
            model.ActiveCode = string.Join("", model.ActiveCodeArr);

            if (model.userid == 0 || string.IsNullOrEmpty(model.ActiveCode) || model.ActiveCode.Length != 5)
            {
                TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
                TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);
                return View();
            }
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //                 _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}


            var user = _userService.GetUserById(model.userid);
            if (user != null && user.PhoneActiveCode.ToString() == model.ActiveCode)
            {
                if (user.PhoneActiveCodeExpDate < DateTime.Now)
                {
                    ModelState.AddModelError("ActiveCode", "کد تایید شما منقضی شده است.");
                    TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
                    TempData["Phone"] = JsonConvert.SerializeObject(user.Phone);
                    return View(model);
                }
                user.PhoneActiveCode = CodeGenerator.PhoneActiveCodeGenerator();

                if (_userService.EditUser(user))
                {

                    var claims = new List<Claim>
                        {
                            new Claim("userid", user.Id.ToString()),
                            new Claim("name", String.IsNullOrEmpty(user.FullName) ? "کاربر جدید" : user.FullName),
                            new Claim("phone", user.Phone),
                            new Claim("date", user.CreateDate.GetMonthPersian()),
                            new Claim("avatar", user.Avatar.ToString()),
                            new Claim("IsColleauge", user.IsColleague.ToString())
                        };

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = false
                    };
                    HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")), properties);
                    ChangeCartCookieToUser(user.Id);
                    return RedirectToAction("Index", "Profile", new { area = "User" });
                }
            }
            //ModelState.AddModelError("ActiveCode", "کد تایید وارد شده صحیح نمیباشد");
            ModelState.AddModelError("", "کد تایید وارد شده صحیح نمیباشد");
            TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
            TempData["Phone"] = JsonConvert.SerializeObject(user.Phone);
            return View(model);
        }

        [Route("ConfirmPhoneColleague")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SanitizeInput]

        public async Task<IActionResult> ConfirmPhoneColleague(ActivePhoneViewModel model)
        {
            TempData["Type"] = model.Type;

            model.ActiveCode = string.Join("", model.ActiveCodeArr);

            if (model.userid == 0 || string.IsNullOrEmpty(model.ActiveCode) || model.ActiveCode.Length != 5)
            {
                TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
                TempData["Phone"] = JsonConvert.SerializeObject(model.Phone);
                return View();
            }
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //                 _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}


            var user = _cooperationRequestService.FindById(model.userid);
            if (user != null && user.PhoneActiveCode.ToString() == model.ActiveCode)
            {
                if (user.PhoneActiveCodeExpDate < DateTime.Now)
                {
                    ModelState.AddModelError("ActiveCode", "کد تایید شما منقضی شده است.");
                    TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
                    TempData["Phone"] = JsonConvert.SerializeObject(user.ShomareTamas);
                    return View(model);
                }
                user.PhoneActiveCode = CodeGenerator.PhoneActiveCodeGenerator();

                if (_cooperationRequestService.EditRequest(user))
                {
                    TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
                    TempData["Phone"] = JsonConvert.SerializeObject(user.ShomareTamas);
                    if (model.Type == EnumRealLegal.Real)
                        return RedirectToAction("RegisterColleagueReal");
                    else
                        return RedirectToAction("RegisterColleagueLegal");



                }
            }
            //ModelState.AddModelError("ActiveCode", "کد تایید وارد شده صحیح نمیباشد");
            ModelState.AddModelError("", "کد تایید وارد شده صحیح نمیباشد");
            TempData["UserId"] = JsonConvert.SerializeObject(model.userid);
            TempData["Phone"] = JsonConvert.SerializeObject(user.ShomareTamas);
            return View(model);
        }

        private void ChangeCartCookieToUser(long userId)
        {
            if (Request.Cookies["Eshopcartcookie"] != null)
            {
                var cookie = Request.Cookies["Eshopcartcookie"];
                var cart = _cartService.GetCartByCookie(cookie);
                if (cart != null)
                {
                    var userCart = _cartService.GetCartByUserId((int)userId);
                    if (userCart != null)
                    {
                        var res2 = _cartService.RemoveCartAndCartDetail(userCart);
                    }
                    cart.Coockie = null;
                    cart.UserId = userId;
                    var res = _cartService.UpdateCart(cart);
                }
            }
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]

        public IActionResult GetCity(int id)
        {
            return Json(_addressService.GetCityByProvinceId(id));
        }
        #endregion


        #region Forget

        #endregion

        public IActionResult IsLocalUrl(string returnurl)
        {
            return Redirect(Url.IsLocalUrl(returnurl) ? returnurl : "/");
        }
    }
}
