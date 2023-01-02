using Microsoft.AspNetCore.Mvc;
using Project4_Nhom3.Models;
using RepositoryLayer;
using ServiceLayer.Service;

namespace Project4_Nhom3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly DataDbContext _context;
        public AccountController(IRegisterService registerService, DataDbContext context)
        {
            _registerService = registerService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModal register = new RegisterViewModal();
            ViewBag.MessageRegister = "";
            return View(register);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModal register)
        {
            ViewBag.MessageRegister = "";
            var dangKy = new RegisterViewModal();
            if (ModelState.IsValid)
            {
                if (_registerService.isExistAccount(register.UserName))
                {
                    register.UserName = "";
                    ViewBag.MessageRegister += "Tài khoản đã tồn tại!";
                    return View(register);
                }
                if(!_registerService.isValidPassword(register.Password))
                {
                    register.Password = "";
                    ViewBag.MessageRegister += "Mật khẩu không đúng định dạng!";
                    return View(register);
                }
                _registerService.RegisterAccount(register);
            }
            return View(register);
        }
    }
}
