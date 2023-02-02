using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class BannerVM : Banner
    {
        public IFormFile ImageURL { get; set; }
    }
}
