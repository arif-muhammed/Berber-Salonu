using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Range(1, 120, ErrorMessage = "Duration must be between 1 and 120 minutes")]
        public int Duration { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
