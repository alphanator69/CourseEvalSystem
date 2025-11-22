using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;
using CourseEvaluationSystem.Controllers;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace CourseEvaluationSystem.Tests
{
    public class CourseControllerTests
    {
        private ApplicationDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsAllCourses()
        {
            var context = GetInMemoryDb();
            context.Courses.Add(new Course { Title = "Math" });
            context.Courses.Add(new Course { Title = "History" });
            await context.SaveChangesAsync();

            var controller = new CourseController(context);

            var result = await controller.Index() as ViewResult;
            var model = result?.Model as List<Course>;

            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Create_AddsCourse()
        {
            var context = GetInMemoryDb();
            var controller = new CourseController(context);

            var course = new Course { Title = "Physics" };

            var result = await controller.Create(course) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Courses);
        }
    }
}
