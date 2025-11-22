using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CourseEvaluationSystem.Data;
using CourseEvaluationSystem.Models;
using CourseEvaluationSystem.Controllers;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace CourseEvaluationSystem.Tests
{
    public class EvaluationControllerTests
    {
        private ApplicationDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task MyEvaluations_ReturnsEvaluationsForStudent()
        {
            var context = GetInMemoryDb();

            var student = new Student { ID = 1, Name = "Test Student", Email = "test@student.com" };
            context.Students.Add(student);

            var course = new Course { ID = 1, Title = "History" };
            context.Courses.Add(course);

            context.Evaluations.Add(new Evaluation
            {
                ID = 1,
                CourseID = course.ID,
                StudentID = student.ID,
                Rating = 4,
                Comment = "Good"
            });

            await context.SaveChangesAsync();

            var controller = new EvaluationController(context);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Items["StudentID"] = student.ID;

            var result = await controller.MyEvaluations() as ViewResult;
            var model = result?.Model as List<Evaluation>;

            Assert.NotNull(model);
            Assert.Single(model);
            Assert.Equal(4, model[0].Rating);
            Assert.Equal("Good", model[0].Comment);
        }

        [Fact]
        public async Task Create_AddsEvaluation()
        {
            var context = GetInMemoryDb();
            var controller = new EvaluationController(context);

            var course = new Course { Title = "Chemistry" };
            context.Courses.Add(course);
            await context.SaveChangesAsync();

            var evaluation = new Evaluation
            {
                CourseID = course.ID,
                Rating = 5,
                Comment = "Excellent"
            };

            var result = await controller.Create(evaluation) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Evaluations);
        }
    }
}
