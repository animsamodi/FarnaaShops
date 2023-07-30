using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        IUserService _userService; 
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserController(IUserService userService, IConfiguration config,
            IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult notaccess()
        {
            return View();
        }

        public IActionResult Login()

        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = _userService.GetUserById(Convert.ToInt64(userId));
                    if (user != null)
                        if (user.TypeUser == EnumTypeUser.Admin && user.IsActive)
                            return RedirectToAction("Index", "Home");
                }


            }
            HttpContext.SignOutAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    if (!await GoogleRecaptchaHelper.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"],
            //                  _config["GoogleReCaptcha:secret"]))
            //    {
            //        ModelState.AddModelError(string.Empty, "لطفا احراز هویت انجام دهید");
            //        return View(model);
            //    }
            //}

            var user = _userService.LoginUser(model.Email_Phone);
            if (user != null)
            {
                if (user.TypeUser != EnumTypeUser.Admin)
                {
                    ModelState.AddModelError("", "کاربر گرامی! شما  دسترسی برای ورود را ندارید");
                    return View(model);

                }

                if (user.IsActive)
                {
                    string[] tempstring = user.Password.Split("-");
                    byte[] hashpassword = new byte[tempstring.Length];
                    for (int i = 0; i < tempstring.Length; i++)
                        hashpassword[i] = Convert.ToByte(tempstring[i]);
                    if (PasswordHash.VerifyHashedPasswordV2(hashpassword, model.Password))
                    {
                        var permissons = _userService.GetRolePermissonsByRoleId(user.RoleId).Select(r => r.Permisson.Code).ToList();
                        permissons.Add((int)EnumPermission.Operator);
                        var claims = new List<Claim>
                        {
                            new Claim("userid",user.Id.ToString()),
                            new Claim("name",String.IsNullOrEmpty(user.FullName) ? user.Phone : user.FullName),
                            new Claim("avatar",user.Avatar.ToString()),
                            new Claim("permissons",Newtonsoft.Json.JsonConvert.SerializeObject(permissons))
                        };

                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),


                        };
                        HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")), properties);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "کاربری با این مشخصات پیدا نشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "حساب کابری شما فعال نیست");
                }
            }
            else
            {
                ModelState.AddModelError("", "کاربری با این مشخصات پیدا نشد");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult RoleList()
        {
            return View(_userService.GetRoles());
        }
        [Authorize]
        public IActionResult UserList()
        {
            return View(_userService.GetUserAdmins());
        }

        [Authorize]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = _userService.GetRoles();

            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateUser(UserAdminViewModel adminViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _userService.GetRoles();
                return View(adminViewModel);
            }


            bool res = _userService.AddUserAdmin(adminViewModel);
            TempData["res"] = res ? "success" : "faild";
            if (res)
                return RedirectToAction(nameof(UserList));

            ModelState.AddModelError("", "در ثبت اطلاعات خطایی رخ داده است");
            ViewBag.Roles = _userService.GetRoles();
            return View(adminViewModel);

        }



        [Authorize]
        public IActionResult CreateRole()
        {
            return View(_userService.GetPermissons());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult CreateRole(string name, List<int> permissons)
        {
            Role role = new Role
            {
                Name = name,
            };
            var roleid = _userService.AddRole(role);
            List<RolePermisson> rolePermissons = new List<RolePermisson>();
            foreach (var item in permissons)
            {
                rolePermissons.Add(new RolePermisson
                {
                    RoleId = roleid,
                    PermissonId = item
                });
            }
            _userService.AddRolePermissons(rolePermissons);
            return RedirectToAction(nameof(RoleList));
        }

        public IActionResult EditRole(int id)
        {
            var role = _userService.GetRoleAndPermissonsForEdit(id);
            role.Permissons = _userService.GetPermissons();
            return View(role);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditRole(EditRoleViewModel editRoleViewModel, List<int> permissons)
        {
            Role role = new Role
            {
                Id = editRoleViewModel.RoleId,
                Name = editRoleViewModel.Name,
            };
            _userService.UpdateRole(role);
            var oldrolepermissons = _userService.GetRolePermissonsByRoleId(editRoleViewModel.RoleId);
            if (oldrolepermissons.Count > 0)
                _userService.RemoveRolePermissons(oldrolepermissons);
            List<RolePermisson> rolePermissons = new List<RolePermisson>();
            foreach (var item in permissons)
            {
                rolePermissons.Add(new RolePermisson
                {
                    RoleId = editRoleViewModel.RoleId,
                    PermissonId = item
                });
            }
            _userService.AddRolePermissons(rolePermissons);
            return RedirectToAction(nameof(RoleList));
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }




    }
}