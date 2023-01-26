using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;

namespace RepositoryLayer
{
    public class UserVM : Users
    {
        public IFormFile ImageURL { get; set; }
    }
}
