using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace project.Controllers
{
    public class ServicesController : Controller
    {
        private readonly SalonDbContext _context;

        public ServicesController(SalonDbContext context)
        {
            _context = context;
        }

        // عرض جميع الخدمات
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.Include(s => s.Employees).ToListAsync();
            return View(services);
        }

        // عرض صفحة إنشاء خدمة جديدة
        public IActionResult Create()
        {
            ViewData["Employees"] = _context.Employees1.ToList(); // تحميل قائمة الموظفين
            return View();
        }

        // معالجة إنشاء خدمة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Duration")] Service service, int[] selectedEmployees)
        {
            // طباعة قائمة الموظفين المحددين إلى وحدة التحكم
            Console.WriteLine($"Selected Employees: {string.Join(", ", selectedEmployees ?? new int[0])}");

            if (ModelState.IsValid)
            {
                if (selectedEmployees != null && selectedEmployees.Length > 0)
                {
                    // ربط الموظفين بالخدمة
                    service.Employees = _context.Employees1
                                                .Where(e => selectedEmployees.Contains(e.Id))
                                                .ToList();
                }

                // حفظ الخدمة في قاعدة البيانات
                _context.Add(service);
                await _context.SaveChangesAsync();

                // إعادة التوجيه إلى صفحة Index
                return RedirectToAction(nameof(Index));
            }

            // طباعة أخطاء النموذج إلى وحدة التحكم
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
            }

            // إعادة تحميل الموظفين في حالة وجود أخطاء
            ViewData["Employees"] = _context.Employees1.ToList();
            return View(service);
        }


        // عرض صفحة تعديل الخدمة
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            ViewData["Employees"] = _context.Employees1.ToList();
            return View(service);
        }

        // معالجة تعديل الخدمة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Duration")] Service service, int[] selectedEmployees)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingService = await _context.Services.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);

                    if (existingService == null)
                    {
                        return NotFound();
                    }

                    // تحديث البيانات الأساسية
                    existingService.Name = service.Name;
                    existingService.Price = service.Price;
                    existingService.Duration = service.Duration;

                    // تحديث الموظفين المرتبطين بالخدمة
                    if (selectedEmployees != null)
                    {
                        // إزالة جميع الموظفين الحاليين وإضافة الموظفين المحددين
                        existingService.Employees.Clear();
                        existingService.Employees = _context.Employees1.Where(e => selectedEmployees.Contains(e.Id)).ToList();
                    }
                    else
                    {
                        existingService.Employees.Clear(); // إذا لم يتم تحديد موظفين
                    }

                    // حفظ التعديلات
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

            ViewData["Employees"] = _context.Employees1.ToList();
            return View(service);
        }

        // عرض صفحة حذف الخدمة
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // تأكيد حذف الخدمة
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.Include(s => s.Employees).FirstOrDefaultAsync(s => s.Id == id);
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
