using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;

namespace CourseEvaluationSystem.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Visa formulär för evaluation
        public async Task<IActionResult> Create(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return NotFound();

            ViewBag.CourseTitle = course.Title;
            ViewBag.CourseId = course.ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Comment,CourseID")] Evaluation evaluation)
        {
            if (!ModelState.IsValid) return View(evaluation);

            // Teststudent
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == 1);
            if (student == null)
            {
                student = new Student { Name = "Test Student", Email = "test@test.com" };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            evaluation.StudentID = student.ID;
            evaluation.Date = DateTime.Now;

            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyEvaluations));
        }

        public async Task<IActionResult> MyEvaluations()
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == 1);
            if (student == null) return View(new List<Evaluation>());

            var evaluations = await _context.Evaluations
                .Include(e => e.Course)
                .Where(e => e.StudentID == student.ID)
                .ToListAsync();

            return View(evaluations);
        }
    }
}
