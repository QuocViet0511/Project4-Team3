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

        public LoginController(IUserService userService)
        {
            _userService= userService;
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
                var result = _userService.Login(loginViewModel.UserName,loginViewModel.Password);
                if (result == 1)
                {
                    var user = _userService.GetUserByName(loginViewModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.Id;
                    userSession.UserName = user.UserName;
                    //Session.Add(CommonConstands.ADMIN_SESSION, userSession);
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
