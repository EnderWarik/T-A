using System.ComponentModel.DataAnnotations;

namespace TitsAndAss.Models
{
    public class ProductPost
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int FromUser { get; set; }
    }
}
