using System.ComponentModel.DataAnnotations;

namespace ConvercionPortal.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
