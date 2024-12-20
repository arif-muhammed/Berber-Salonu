using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class ServicesController : Controller
    {
        private readonly SalonDbContext _context;

        public ServicesController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(string searchString, int? minDuration, int? maxDuration, decimal? minPrice, decimal? maxPrice)
        {
            // جلب البيانات
            var services = _context.Services.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrEmpty(searchString))
            {
                services = services.Where(s => s.Name.Contains(searchString));
            }
            if (minDuration.HasValue)
            {
                services = services.Where(s => s.Duration >= minDuration);
            }
            if (maxDuration.HasValue)
            {
                services = services.Where(s => s.Duration <= maxDuration);
            }
            if (minPrice.HasValue)
            {
                services = services.Where(s => s.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                services = services.Where(s => s.Price <= maxPrice);
            }

            // تمرير القيم إلى ViewData
            ViewData["searchString"] = searchString;
            ViewData["minDuration"] = minDuration;
            ViewData["maxDuration"] = maxDuration;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;

            return View(await services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Duration,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Duration,Price")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
