using System;

namespace DomainLayer.Models
{
    public class KeySP : BaseEntity
    {
        public int SanPhamId { get; set; }
        public string KeyName { get; set; }
        public string KeyInfo { get; set; }
        public DateTime? NgayTao { get; set; }
        public byte TrangThaiKey { get; set; }

    }
}
