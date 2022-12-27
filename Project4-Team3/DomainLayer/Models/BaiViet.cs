using System;

namespace DomainLayer.Models
{
    public class BaiViet : BaseEntity
    {
        public string ChuDe { get; set; }
        public string NoiDung { get; set; }
        public string Image { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public int UserId { get; set; }

    }
}
