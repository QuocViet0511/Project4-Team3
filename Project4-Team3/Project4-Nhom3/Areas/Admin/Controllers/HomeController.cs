using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project4_Nhom3.Common;
using ServiceLayer.Service;
using System;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public HomeController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var userSession = _session.GetString(CommonConstands.ADMIN_SESSION);
            ViewBag.userName = userSession;
            if (string.IsNullOrEmpty(userSession))
            {
                return Redirect("~/Admin/Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                _session.SetString(CommonConstands.ADMIN_SESSION, "");
                return Redirect("~/Admin/Login");
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
