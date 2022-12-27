using DomainLayer.Models;
using RepositoryLayer;
using System.Collections.Generic;

namespace ServiceLayer.Service
{
    public interface IUserService
    {
        IEnumerable<Users> GetAll();
        Users GetUser(int id);
        void InsertUser(Users user);
        void DeleteUser(int id);
        void UpdateUser(Users user);
    }
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _repository;
        public UserService(IRepository<Users> repository)
        {
            _repository = repository;
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
