using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 480, ErrorMessage = "Duration must be between 1 and 480 minutes")]
        public int Duration { get; set; }

        public ICollection<Employee1> Employees { get; set; } = new List<Employee1>();

    }
}
