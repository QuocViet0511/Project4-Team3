using System;

namespace DomainLayer.Models
{
    public class GiamGia : BaseEntity
    {
        public string Name { get; set; }
        public string ThongTin { get; set; }
        public decimal? PhanTramGiamGia { get; set; }
        public byte TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
    }
}
