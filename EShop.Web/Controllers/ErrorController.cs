using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}