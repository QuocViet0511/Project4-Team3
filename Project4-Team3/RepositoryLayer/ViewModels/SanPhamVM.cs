using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepositoryLayer
{
    public class SanPhamVM : SanPham
    {
        public string TenDanhMucSanPham { get; set; }
        public string MaGiamGia { get; set; }
        public string TenKey { get; set; }
        [AllowHtml]
        public IFormFile ImageURL { get; set; }
        public List<GiamGia> listGiamGia { get; set; }
        public List<KeySP> listKeySP { get; set; }
        public List<DanhMucSanPham> listDMSP { get; set; }
    }
}
