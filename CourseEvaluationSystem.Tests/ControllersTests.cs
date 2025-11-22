using Xunit;
using Microsoft.EntityFrameworkCore;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;
using CourseEvaluationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CourseEvaluationSystem.Tests
{
    public class CourseControllerTests
    {
        private ApplicationDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsCourses()
        {
            var context = GetInMemoryDb();
            context.Courses.Add(new Course { Title = "Math 101" });
            context.Courses.Add(new Course { Title = "History 101" });
            await context.SaveChangesAsync();

            var controller = new CourseController(context);

            var result = await controller.Index() as ViewResult;
            var model = result?.Model as List<Course>;  

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Create_PostAddsCourse()
        {
            var context = GetInMemoryDb();
            var controller = new CourseController(context);
            var newCourse = new Course { Title = "Physics 101" };

            var result = await controller.Create(newCourse) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Courses);
        }
    }
}
