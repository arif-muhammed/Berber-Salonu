using Microsoft.EntityFrameworkCore;
using project.Data; // المساحة الخاصة ب DbContext

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // قراءة سلسلة الاتصال من appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // إضافة DbContext وربطه مع SQL Server
            builder.Services.AddDbContext<SalonDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}