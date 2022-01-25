using System.ComponentModel.DataAnnotations;

namespace TitsAndAss.Models
{
    public class ProductOrder
    {
        [Required]
        public int IdOrder { get; set; }
        [Required]
        public int IdProduct { get; set; }
        
    }
}
