using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IBinhLuanService
    {
        IEnumerable<BinhLuan> GetAll();
        BinhLuan GetBinhLuan(int id);
        void InsertBinhLuan(BinhLuan binhLuan);
        void DeleteBinhLuan(int id);
        void UpdateBinhLuan(BinhLuan binhLuan);
    }
    public class BinhLuanService : IBinhLuanService
    {
        private readonly IRepository<BinhLuan> _repository;
        public BinhLuanService(IRepository<BinhLuan> repository)
        {
            _repository = repository;
        }

        public void DeleteBinhLuan(int id)
        {
            BinhLuan binhLuan = GetBinhLuan(id);
            _repository.Delete(binhLuan);
        }

        public IEnumerable<BinhLuan> GetAll()
        {
            return _repository.GetAll();
        }

        public BinhLuan GetBinhLuan(int id)
        {
            return _repository.Get(id);
        }

        public void InsertBinhLuan(BinhLuan binhLuan)
        {
            _repository.Insert(binhLuan);
        }

        public void UpdateBinhLuan(BinhLuan binhLuan)
        {
            _repository.Update(binhLuan);
        }
    }
}
