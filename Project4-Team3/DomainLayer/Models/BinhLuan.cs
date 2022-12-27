using System;

namespace DomainLayer.Models
{
    public class BinhLuan : BaseEntity
    {
        public int UserId { get; set; }
        public int SanPhamId { get; set; }
        public int ParentId { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
