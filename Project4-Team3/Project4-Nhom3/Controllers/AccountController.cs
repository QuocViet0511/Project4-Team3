using DomainLayer.Models;
using DomainLayer.ViewModal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project4_Nhom3.Areas.Admin.Models;
using Project4_Nhom3.Common;
using RepositoryLayer;
using ServiceLayer.Service;

namespace Project4_Nhom3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AccountController(IRegisterService registerService, DataDbContext context, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _registerService = registerService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var register = new Users();
            ViewBag.MessageRegister = "";
            return View(register);
        }

        [HttpPost]
        public IActionResult Register(Users register)
        {
            ViewBag.MessageRegister = "";
            var dangKy = new Users();
            if (ModelState.IsValid)
            {
                if (_registerService.isExistAccount(register.UserName))
                {
                    register.UserName = "";
                    ViewBag.MessageRegister += "Tài khoản đã tồn tại!";
                    return View(register);
                }
                if (!_registerService.isValidPassword(register.Password))
                {
                    register.Password = "";
                    ViewBag.MessageRegister += "Mật khẩu không đúng định dạng!";
                    return View(register);
                }
                _registerService.RegisterAccount(register);
                ViewData["SuccessMsg"] = "Đăng ký thành công";
                register = new Users();
            }
            return View(register);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Users loginViewModel = new Users();
            ViewBag.MessageLogin = "";
            return View(loginViewModel);
        }


        [HttpPost]
        public ActionResult Login(Users loginViewModel)
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                var result = _userService.LoginUser(loginViewModel.UserName, loginViewModel.Password);
                if (result == 1)
                {
                    var user = _userService.GetUserByName(loginViewModel.UserName);
                    var userSession = new Users();
                    userSession.Id = user.Id;
                    userSession.UserName = user.UserName;
                    _session.SetString(CommonConstands.USER_SESSION, userSession.UserName);
                    return Redirect("~/");
                }
                else if (result == 0)
                {
                    ViewBag.MessageLogin += "Tài khoản không tồn tại";
                }
                else if (result == -1)
                {
                    ViewBag.MessageLogin += "Tài khoản đang bị khóa";
                }
                else if (result == -2)
                {
                    ViewBag.MessageLogin += "Mật khẩu không đúng";
                }
                else
                {
                    ViewBag.MessageLogin += "Đăng nhập không thành công";
                }
            }
            return View(loginViewModel);
        }

        public ActionResult Logout()
        {
            _session.SetString(CommonConstands.USER_SESSION, "");
            return Redirect("~/");
        }

        public ActionResult Infor()
        {
            UserVM userVM = new UserVM();
            var user = _userService.GetUserByName(_session.GetString(CommonConstands.USER_SESSION));
            if (user != null)
            {
                userVM.UserName = user.UserName;
                userVM.Email = user.Email;
                userVM.Phone = user.Phone;
                userVM.Password = user.Password;
                userVM.Avatar = user.Avatar;
                userVM.Id = user.Id;
            }
            else
            {
                return Redirect("~/");
            }
            return View(userVM);
        }

    }
}
