using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]

    public class HomeController : BaseAdminController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
