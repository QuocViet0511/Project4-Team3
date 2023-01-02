using DomainLayer.Models;
using DomainLayer.ViewModal;
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
        void RegisterAccount(Users register);
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

        public void RegisterAccount(Users register)
        {
            var dangKy = new Users
            {
                UserName = register.UserName,
                Password = register.Password,
                NgayTao = DateTime.Now,
                Email = register.Email,
                Phone = register.Phone,
                RoleId = 2,
            };
            _context.Add(dangKy);
            _context.SaveChanges();
        }
    }
}
