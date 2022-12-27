using Microsoft.AspNetCore.Mvc;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Home/Index.cshtml");
        }
    }
}
