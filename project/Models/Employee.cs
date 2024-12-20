using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Expertise is required")]
        [StringLength(200, ErrorMessage = "Expertise cannot exceed 200 characters")]
        public string Expertise { get; set; }

        [Required(ErrorMessage = "Available hours are required")]
        [StringLength(50, ErrorMessage = "Available hours cannot exceed 50 characters")]
        public string AvailableHours { get; set; }
    }
}