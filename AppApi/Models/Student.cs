using System.ComponentModel.DataAnnotations;

namespace AppApi.Models
{
    public class Student : BaseEntity
    {
        [MaxLength(20)]
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int? Age { get; set; }
    }
}
