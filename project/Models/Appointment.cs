using System.ComponentModel.DataAnnotations;
using project.Attributes; // تأكد من أن المسار مطابق للمجلد الذي أنشأت فيه الـ Attribute
using project.Models;

namespace project.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public int CustomerId { get; set; } // Foreign Key
        public User Customer { get; set; } // Navigation Property

        [Required(ErrorMessage = "Service is required")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required(ErrorMessage = "Employee1 is required")]
        public int EmployeeId { get; set; }
        public Employee1 Employee { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentTime { get; set; }
    }
}