using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CourseEvaluationSystem.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return NotFound();
            ViewBag.CourseId = course.ID;
            ViewBag.CourseTitle = course.Title;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Evaluation evaluation)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == 1);
            if (student == null)
            {
                student = new Student { Name = "Test Student", Email = "test@test.com" };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            evaluation.StudentID = student.ID;
            evaluation.Date = System.DateTime.Now;

            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> MyEvaluations()
        {
            var student = await _context.Students.Include(s => s.Evaluations)
                                                 .ThenInclude(e => e.Course)
                                                 .FirstOrDefaultAsync(s => s.ID == 1);
            if (student == null) return View(new List<Evaluation>());
            return View(student.Evaluations);
        }
    }
}
