using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Data
{
    public class SalonDbContext : DbContext
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Employee1> Employees1 { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; } // جدول المستخدمين

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تعطيل Cascade Delete على العلاقات
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict); // تعيين DeleteBehavior.Restrict

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // تعيين DeleteBehavior.Restrict

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.Employees)
                .WithMany(e => e.Services)
                .UsingEntity(j => j.ToTable("EmployeeServices")); // تحديد اسم جدول الربط
        }
    }
}
