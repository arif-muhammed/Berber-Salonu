using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;
using System.Diagnostics;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalonDbContext _context;

        public HomeController(ILogger<HomeController> logger, SalonDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var employeeCount = _context.Employees.Count();
            var serviceCount = _context.Services.Count();
            var appointmentCount = _context.Appointments.Count();

            ViewData["EmployeeCount"] = employeeCount;
            ViewData["ServiceCount"] = serviceCount;
            ViewData["AppointmentCount"] = appointmentCount;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
