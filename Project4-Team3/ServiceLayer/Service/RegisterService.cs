using DomainLayer.Models;
using Project4_Nhom3.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IRegisterService
    {
        bool isExistAccount(string account);

        bool isValidPassword(string password);
        void RegisterAccount(RegisterViewModal register);
    }
    public class RegisterService : IRegisterService
    {
        private readonly DataDbContext _context;
        public RegisterService(DataDbContext context)
        {
            _context = context;
        }

        public bool isExistAccount(string account)
        {
            var check = (from us in _context.Users
                         where us.UserName == account
                         select us).SingleOrDefault();
            if(check != null)
            {
                return true;
            }
            return false;
        }

        public bool isValidPassword(string password)
        {
            return Regex.IsMatch(password, @"\w");
        }

        public void RegisterAccount(RegisterViewModal register)
        {
            var dangKy = new RegisterViewModal
            {
                UserName = register.UserName,
                Password = register.Password,
                CreatedDate = DateTime.Now,
                Email = register.Email,
                Phone = register.Phone,
            };
            _context.Add(dangKy);
            _context.SaveChanges();
        }
    }
}
