using System;

namespace DomainLayer.Models
{
    public class Feedback : BaseEntity
    {
        public string Name { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
