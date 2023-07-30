using System.Linq;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class UserManagementController : BaseAdminController
    {
        IUserService _userService;
        public UserManagementController(IUserService userService, 
            Logging.AuditLog.IAuditService logger,IHttpContextAccessor contextAccessor) : base(logger,contextAccessor)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_userService.GetUserAdmins(), loadOptions);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Post(string values)
        {
            var user = new UserAdminViewModel();
            JsonConvert.PopulateObject(values, user);

            if (!TryValidateModel(user))
                return BadRequest(string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)));

            var existUser = _userService.CheckExistPhone(user.Phone,EnumTypeSystem.Farnaa);
            var existUserPlus = _userService.CheckExistPhone(user.Phone,EnumTypeSystem.FarnaaPlus);
            if (existUser != null || existUserPlus != null)
                return BadRequest(existUser.TypeUser == EnumTypeUser.User
                    ? "کاربری با این شماره در سایت وجود دارد" 
                    : "شما قبلا اپراتوری با این شماره تعریف کرده اید ");

            TempData["res"] = "faild";
            if (_userService.AddUserAdmin(user))
            {
                _logger.CreateAuditScope(new AuditLog<UserAdminViewModel>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = user,
                });
                TempData["res"] = "success";
            }


            return Ok();
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]
        public IActionResult Put(int key, string values)
        {
            var user = _userService.GetUserAdminById(key);
            JsonConvert.PopulateObject(values, user);

            if (!TryValidateModel(user))
                return BadRequest(string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)));


            TempData["res"] = "faild";
            if (_userService.EditUserAdmin(user))
            {
                _logger.CreateAuditScope(new AuditLog<UserAdminViewModel>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = user,
                });
                TempData["res"] = "success";
            }
            
            return Ok();
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public void Delete(int key)
        {
            _userService.DeleteUserById(key);

            _logger.CreateAuditScope(new AuditLog<long>()
            {
                Modifier = _userId,
                Action = Command.Remove,
                Entite = key,
            });
        }
        [HttpGet]
        public object GetRole(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_userService.GetRoles(), loadOptions);
        }

    }
}