using System;

namespace DomainLayer.Models
{
    public class KeySP : BaseEntity
    {
        public string KeyName { get; set; }
        public string KeyInfo { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThaiKey { get; set; }

    }
}
