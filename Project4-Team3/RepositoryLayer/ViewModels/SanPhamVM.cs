﻿using DomainLayer.Models;
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
        List<GiamGia> listGiamGia { get; set; }
        List<KeySP> listKeySP { get; set; }
    }
}
