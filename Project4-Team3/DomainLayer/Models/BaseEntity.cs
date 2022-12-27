using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}