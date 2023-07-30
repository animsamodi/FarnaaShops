using System.Linq;
using EShop.Core.Security;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    [Authorize]
    [CheckPermisson((int)EnumPermission.Operator)]
    [AutoValidateAntiforgeryToken]
    public class BaseAdminController : Controller
    {
        public readonly IAuditService _logger;
        public readonly string _userId;
        public BaseAdminController(IAuditService logger,IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _userId = contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
        }

        public BaseAdminController()
        {
            
        }
      
       
    }

}