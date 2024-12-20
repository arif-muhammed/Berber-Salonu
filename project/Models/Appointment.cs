using System.ComponentModel.DataAnnotations;
using project.Attributes; // تأكد من أن المسار مطابق للمجلد الذي أنشأت فيه الـ Attribute
using project.Models;

namespace project.Models
{
	public class Appointment
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Customer Name is required")]
		[StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters")]
		public string CustomerName { get; set; }

		[Required(ErrorMessage = "Service is required")]
		public int ServiceId { get; set; }
		public Service Service { get; set; } // Navigation Property for Service

		[Required(ErrorMessage = "Employee is required")]
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; } // Navigation Property for Employee

		[Required(ErrorMessage = "Appointment time is required")]
		[DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format")]
		[FutureDate(ErrorMessage = "Appointment time must be in the future")]
		public DateTime AppointmentTime { get; set; }
	}
}