using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project4_Nhom3.Areas.Admin.Models;
using Project4_Nhom3.Common;
using ServiceLayer.Service;

namespace Project4_Nhom3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;  
        public LoginController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService= userService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public IActionResult Index()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                var result = _userService.LoginAdmin(loginViewModel.UserName,loginViewModel.Password);
                if (result == 1)
                {
                    var user = _userService.GetUserByName(loginViewModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.Id;
                    userSession.UserName = user.UserName;
                    _session.SetString(CommonConstands.ADMIN_SESSION, userSession.UserName);
                    return Redirect("~/Admin/Home/Index");
                }
                else if (result == 0)
                {
                    ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                }
                else if (result == -1)
                {
                    ViewBag.ErrorMessage = "Tài khoản này không đủ quyền truy cập trang Admin";
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";

            }
            return View(loginViewModel);
        }
    }
}
