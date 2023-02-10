using Microsoft.AspNetCore.Mvc;

namespace Project4_Nhom3.Controllers
{
    public class LienHeController : Controller
    {
        public IActionResult Index()
        {
             return View("index");
        }
    }
}
