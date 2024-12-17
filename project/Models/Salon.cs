namespace project.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OpeningHours { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
