using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public interface IBannerService
    {
        IEnumerable<Banner> GetAll();
        Banner GetBanner(int id);
        void InsertBanner(Banner Banner);
        void DeleteBanner(int id);
        void UpdateBanner(Banner Banner);
    }
    public class BannerService : IBannerService
    {
        private readonly IRepository<Banner> _repository;
        public BannerService(IRepository<Banner> repository)
        {
            _repository = repository;
        }

        public void DeleteBanner(int id)
        {
            Banner banner = GetBanner(id);
            _repository.Delete(banner);
        }

        public IEnumerable<Banner> GetAll()
        {
            return _repository.GetAll();
        }

        public Banner GetBanner(int id)
        {
            return _repository.Get(id);
        }

        public void InsertBanner(Banner banner)
        {
            _repository.Insert(banner);
        }

        public void UpdateBanner(Banner banner)
        {
            _repository.Update(banner);
        }
    }
}
