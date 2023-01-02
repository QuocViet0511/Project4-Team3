using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface ISanPhamService
    {
        IEnumerable<SanPham> GetAll();
        SanPham GetSanPham(int id);
        void InsertSanPham(SanPham sanPham);
        void DeleteSanPham(int id);
        void UpdateSanPham(SanPham sanPham);
    }
    public class SanPhamService : ISanPhamService
    {
        private readonly IRepository<SanPham> _repository;
        public SanPhamService(IRepository<SanPham> repository)
        {
            _repository = repository;
        }

        public void DeleteSanPham(int id)
        {
            SanPham sanPham = GetSanPham(id);
            _repository.Delete(sanPham);
        }

        public IEnumerable<SanPham> GetAll()
        {
            var list = _repository.GetAll(); 
            return list;
        }

        public SanPham GetSanPham(int id)
        {
            return _repository.Get(id);
        }

        public void InsertSanPham(SanPham sanPham)
        {
            _repository.Insert(sanPham);
        }

        public void UpdateSanPham(SanPham sanPham)
        {
            _repository.Update(sanPham);
        }
    }
}
