using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace project.Data
{
    public class SalonDbContextFactory : IDesignTimeDbContextFactory<SalonDbContext>
    {
        public SalonDbContext CreateDbContext(string[] args)
        {
            تحميل التهيئة من ملف appsettings.json
           var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SalonDbContext>();

            استخدام SQL Server والاتصال من appsettings.json
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new SalonDbContext(optionsBuilder.Options);
        }
    }
}
