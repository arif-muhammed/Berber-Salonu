namespace project.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public string AvailableHours { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }

}
