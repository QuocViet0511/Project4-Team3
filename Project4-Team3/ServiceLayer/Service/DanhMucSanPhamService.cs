using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IDanhMucSanPhamService
    {
        IEnumerable<DanhMucSanPham> GetAll();
        DanhMucSanPham GetDanhMucSanPham(int id);
        void InsertDanhMucSanPham(DanhMucSanPham danhMucSanPham);
        void DeleteDanhMucSanPham(int id);
        void UpdateDanhMucSanPham(DanhMucSanPham danhMucSanPham);
    }
    public class DanhMucSanPhamService : IDanhMucSanPhamService
    {
        private readonly IRepository<DanhMucSanPham> _repository;
        public DanhMucSanPhamService(IRepository<DanhMucSanPham> repository)
        {
            _repository = repository;
        }

        public void DeleteDanhMucSanPham(int id)
        {
            DanhMucSanPham danhMucSanPham = GetDanhMucSanPham(id);
            _repository.Delete(danhMucSanPham);
        }

        public IEnumerable<DanhMucSanPham> GetAll()
        {
            return _repository.GetAll();
        }

        public DanhMucSanPham GetDanhMucSanPham(int id)
        {
            return _repository.Get(id);
        }

        public void InsertDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            _repository.Insert(danhMucSanPham);
        }

        public void UpdateDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            _repository.Update(danhMucSanPham);
        }
    }
}
