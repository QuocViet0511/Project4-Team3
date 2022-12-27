using System;

namespace DomainLayer.Models
{
    public class Users : BaseEntity
    {
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string Avatar { get; set; }

    }
}
