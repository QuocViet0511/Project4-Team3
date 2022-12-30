using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Service
{
    public interface IGioHangService
    {
        IEnumerable<GioHang> GetAll();
		GioHang GetGioHang(int id);
        void InsertGioHang(GioHang gioHang);
        void DeleteGioHang(int id);
        void UpdateGioHang(GioHang gioHang);
    }
    public class GioHangService : IGioHangService
    {
        private readonly IRepository<GioHang> _repository;
        public GioHangService(IRepository<GioHang> repository)
        {
            _repository = repository;
        }
        
        public void DeleteGioHang(int id)
        {
            GioHang gioHang = GetGioHang(id);
            _repository.Delete(gioHang);
        }

		public IEnumerable<GioHang> GetAll()
        {
            return _repository.GetAll();
        }

        public GioHang GetGioHang(int id)
        {
            return _repository.Get(id);
        }
		
		public void InsertGioHang(GioHang gioHang)
        {
            _repository.Insert(gioHang);
        }

        public void UpdateGioHang(GioHang gioHang)
        {
            _repository.Update(gioHang);
        }
    }
}
