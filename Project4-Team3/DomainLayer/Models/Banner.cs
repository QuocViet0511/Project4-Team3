using Microsoft.AspNetCore.Http;
using System;

namespace DomainLayer.Models
{
    public class Banner : BaseEntity
    {
        public IFormFile UrlLink { get; set; }
        public string TieuDe { get; set; }
        public bool isActive { get; set; }
    }
}
