[Fact]
public async Task MyEvaluations_ReturnsEvaluationsForLoggedInStudent()
{
    var context = GetInMemoryDb();

    var student = new Student { ID = 10, Name = "Test Student" };
    context.Students.Add(student);

    var course = new Course { Title = "History" };
    context.Courses.Add(course);

    context.Evaluations.Add(new Evaluation
    {
        CourseID = course.ID,
        StudentID = student.ID,
        Rating = 4
    });

    await context.SaveChangesAsync();

    var controller = new EvaluationController(context);

    ControllerUserMock.MockUser(controller, 10);

    var result = await controller.MyEvaluations() as ViewResult;
    var model = result?.Model as List<Evaluation>;

    Assert.NotNull(model);
    Assert.Single(model);
    Assert.Equal(4, model[0].Rating);
}
