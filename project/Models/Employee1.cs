using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Employee1
    {
        internal List<Employee1> service;

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Available hours are required")]
        [StringLength(50, ErrorMessage = "Available hours cannot exceed 50 characters")]
        public string AvailableHours { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime EndDate { get; set; }

        // تعريف العلاقة Many-to-Many
        // العلاقة Many-to-Many
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
