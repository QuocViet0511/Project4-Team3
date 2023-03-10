using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class SanPhamVM : SanPham
    {
        public string TenDanhMucSanPham { get; set; }
        public string MaGiamGia { get; set; }
        public string TenKey { get; set; }
        public decimal? GiaThayDoi { get; set; }
        public IFormFile ImageURL { get; set; }
        public List<GiamGia> listGiamGia { get; set; }
        public List<KeySP> listKeySP { get; set; }
        public List<DanhMucSanPham> listDMSP { get; set; }
        public List<BannerVM> listBanner { get; set; }
        public GiamGia giamGia { get; set; }
    }
}
