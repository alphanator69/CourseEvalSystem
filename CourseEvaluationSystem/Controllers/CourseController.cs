using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;
using System.Threading.Tasks;

namespace CourseEvaluationSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(c => c.Evaluations).ToListAsync();
            return View(courses);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Title))
                return View(course);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
