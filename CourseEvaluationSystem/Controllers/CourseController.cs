using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;

namespace CourseEvaluationSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Lista alla kurser
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // Visa formulär för ny kurs
        public IActionResult Create() => View();

        // Skapa ny kurs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] Course course)
        {
            if (!ModelState.IsValid) return View(course);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
