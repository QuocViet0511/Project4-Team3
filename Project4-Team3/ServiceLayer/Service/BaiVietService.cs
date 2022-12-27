using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IBaiVietService
    {
        IEnumerable<BaiViet> GetAll();
        BaiViet GetBaiViet(int id);
        void InsertBaiViet(BaiViet baiViet);
        void DeleteBaiViet(int id);
        void UpdateBaiViet(BaiViet baiViet);
    }
    public class BaiVietService : IBaiVietService
    {
        private readonly IRepository<BaiViet> _repository;
        public BaiVietService(IRepository<BaiViet> repository)
        {
            _repository = repository;
        }

        public void DeleteBaiViet(int id)
        {
            BaiViet baiViet = GetBaiViet(id);
            _repository.Delete(baiViet);
        }

        public IEnumerable<BaiViet> GetAll()
        {
            return _repository.GetAll();
        }

        public BaiViet GetBaiViet(int id)
        {
            return _repository.Get(id);
        }

        public void InsertBaiViet(BaiViet baiViet)
        {
            _repository.Insert(baiViet);
        }

        public void UpdateBaiViet(BaiViet baiViet)
        {
            _repository.Update(baiViet);
        }
    }
}
