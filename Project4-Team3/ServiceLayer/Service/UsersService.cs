using DomainLayer.Models;
using RepositoryLayer;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Service
{
    public interface IUserService
    {
        IEnumerable<Users> GetAll();
        Users GetUser(int id);
        void InsertUser(Users user);
        void DeleteUser(int id);
        void UpdateUser(Users user);
        int Login(string name, string pass);
        Users GetUserByName(string name);
    }
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _repository;

        private readonly DataDbContext _context;
        public UserService(IRepository<Users> repository, DataDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public int Login(string name, string pass)
        {
            var taikhoan = _context.Users.SingleOrDefault(x => x.UserName == name);
            if (taikhoan == null)
            {
                return 0;
            }
            else
            {
               
                    if (taikhoan.Password == pass)
                        if (taikhoan.RoleId == 1)
                            return 1;
                        else
                            return -1;
                    else
                        return -2;
            }
        }

        public Users GetUserByName(string Name)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == Name);
        }

        public void DeleteUser(int id)
        {
            Users user = GetUser(id);
            _repository.Delete(user);
        }

        public IEnumerable<Users> GetAll()
        {
            return _repository.GetAll();
        }

        public Users GetUser(int id)
        {
            return _repository.Get(id);
        }

        public void InsertUser(Users user)
        {
            _repository.Insert(user);
        }

        public void UpdateUser(Users user)
        {
            _repository.Update(user);
        }
    }
}
