using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
	public class AppointmentsController : Controller
	{
		private readonly SalonDbContext _context;

		public AppointmentsController(SalonDbContext context)
		{
			_context = context;
		}

		// GET: Appointments
		public async Task<IActionResult> Index(string searchString, DateTime? startDate, DateTime? endDate)
		{
			// جلب البيانات من قاعدة البيانات مع تضمين العلاقات
			var appointments = _context.Appointments
				.Include(a => a.Employee)
				.Include(a => a.Service)
				.AsQueryable();

			// تطبيق البحث إذا كانت هناك كلمة مفتاحية
			if (!string.IsNullOrEmpty(searchString))
			{
				appointments = appointments.Where(a => a.CustomerName.Contains(searchString));
			}

			// تطبيق التصفية إذا تم تحديد تواريخ
			if (startDate.HasValue && endDate.HasValue)
			{
				appointments = appointments.Where(a => a.AppointmentTime >= startDate && a.AppointmentTime <= endDate);
			}

			return View(await appointments.ToListAsync());
		}

		// GET: Appointments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments
				.Include(a => a.Employee)
				.Include(a => a.Service)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		// GET: Appointments/Create
		public IActionResult Create()
		{
			ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
			return View();
		}

		// POST: Appointments/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CustomerName,ServiceId,EmployeeId,AppointmentTime")] Appointment appointment)
		{
			if (ModelState.IsValid)
			{
				_context.Add(appointment);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", appointment.EmployeeId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
			return View(appointment);
		}

		// GET: Appointments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment == null)
			{
				return NotFound();
			}

			ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", appointment.EmployeeId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
			return View(appointment);
		}

		// POST: Appointments/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,ServiceId,EmployeeId,AppointmentTime")] Appointment appointment)
		{
			if (id != appointment.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(appointment);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AppointmentExists(appointment.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction(nameof(Index));
			}

			ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", appointment.EmployeeId);
			ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
			return View(appointment);
		}

		// GET: Appointments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _context.Appointments
				.Include(a => a.Employee)
				.Include(a => a.Service)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (appointment == null)
			{
				return NotFound();
			}

			return View(appointment);
		}

		// POST: Appointments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment != null)
			{
				_context.Appointments.Remove(appointment);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}

		private bool AppointmentExists(int id)
		{
			return _context.Appointments.Any(e => e.Id == id);
		}
	}
}
