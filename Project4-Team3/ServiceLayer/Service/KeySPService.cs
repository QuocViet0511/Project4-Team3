using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IKeySPService
    {
        IEnumerable<KeySP> GetAll();
        KeySP GetKeySP(int id);
        void InsertKeySP(KeySP keySP);
        void DeleteKeySP(int id);
        void UpdateKeySP(KeySP keySP);
    }
    public class KeySPService : IKeySPService
    {
        private readonly IRepository<KeySP> _repository;
        public KeySPService(IRepository<KeySP> repository)
        {
            _repository = repository;
        }

        public void DeleteKeySP(int id)
        {
            KeySP keySP = GetKeySP(id);
            _repository.Delete(keySP);
        }

        public IEnumerable<KeySP> GetAll()
        {
            return _repository.GetAll();
        }

        public KeySP GetKeySP(int id)
        {
            return _repository.Get(id);
        }

        public void InsertKeySP(KeySP keySP)
        {
            _repository.Insert(keySP);
        }

        public void UpdateKeySP(KeySP keySP)
        {
            _repository.Update(keySP);
        }
    }
}
