using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;
using System.Linq;

namespace Hairdresser.Controllers
{
    public class LoginController : Controller
    {
        private readonly SalonDbContext _context;

        public LoginController(SalonDbContext context)
        {
            _context = context;
        }

        // عرض صفحة تسجيل الدخول
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // معالجة طلب تسجيل الدخول
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                // التحقق إذا كان المستخدم أدمن
                if ((login.Username == "admin1@gmail.com" && login.Password == "1234") ||
                    (login.Username == "admin2@gmail.com" && login.Password == "1234"))
                {
                    // تمرير بيانات تسجيل الدخول عبر TempData
                    TempData["Role"] = "Admin";
                    TempData["Username"] = "Admin";
                    return RedirectToAction("Dashboard");
                }

                // التحقق إذا كان المستخدم موجود في قاعدة البيانات
                var user = _context.Users.FirstOrDefault(u => u.Email == login.Username && u.Password == login.Password);

                if (user != null)
                {
                    // تمرير بيانات تسجيل الدخول عبر TempData
                    TempData["Role"] = "User";
                    TempData["Username"] = user.FullName;

                    // الانتقال إلى صفحة UserRegister
                    return RedirectToAction("UserRegister");
                }

                // إذا كانت بيانات تسجيل الدخول غير صحيحة
                ViewBag.Error = "Invalid login credentials.";
                return View();
            }

            return View();
        }

        // عرض صفحة التسجيل
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // التحقق إذا كان البريد الإلكتروني مسجل مسبقًا
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(user);
                }
                try
                {
                    // إضافة المستخدم إلى قاعدة البيانات
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    // التحقق من الحفظ
                    var savedUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (savedUser == null)
                    {
                        ModelState.AddModelError(string.Empty, "Failed to save user. Please try again.");
                        return View(user);
                    }

                    // إعادة التوجيه إلى صفحة تسجيل الدخول
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error saving data: {ex.Message}");
                }
            }

            return View(user);
        }


        // عرض لوحة التحكم (Dashboard)
        public IActionResult Dashboard()
        {
            // التحقق من أن المستخدم هو أدمن
            if (TempData["Role"] as string == "Admin")
            {
                TempData.Keep(); // الاحتفاظ بالبيانات لـ TempData بعد القراءة
                return View();
            }

            return RedirectToAction("Login");
        }

        // عرض صفحة تسجيل المستخدم (UserRegister)
        public IActionResult UserRegister()
        {
            // التحقق من أن المستخدم هو User
            if (TempData["Role"] as string == "User")
            {
                ViewBag.Username = TempData["Username"] as string;
                TempData.Keep(); // الاحتفاظ بالبيانات لـ TempData بعد القراءة
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
