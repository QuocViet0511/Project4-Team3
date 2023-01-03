using System;

namespace DomainLayer.Models
{
    public class DanhMucSanPham : BaseEntity
    {
        public string TenDanhMuc { get; set; }
        public string ThongTin { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }

        public void SetNgayTao()
        {
            this.NgayTao = DateTime.Now;
        }
        public void SetNgaySua()
        {
            this.NgaySua = DateTime.Now;
        }
    }
}
