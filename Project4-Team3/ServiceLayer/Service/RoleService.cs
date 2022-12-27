using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetRole(int id);
        void InsertRole(Role role);
        void DeleteRole(int id);
        void UpdateRole(Role role);
    }
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public void DeleteRole(int id)
        {
            Role role = GetRole(id);
            _repository.Delete(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return _repository.GetAll();
        }

        public Role GetRole(int id)
        {
            return _repository.Get(id);
        }

        public void InsertRole(Role role)
        {
            _repository.Insert(role);
        }

        public void UpdateRole(Role role)
        {
            _repository.Update(role);
        }
    }
}
