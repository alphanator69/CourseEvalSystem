using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseEvaluationSystem.Models
{
    public class Evaluation
{
    public int ID { get; set; }
    public int Rating { get; set; }
    public required string Comment { get; set; }
    public DateTime Date { get; set; }

    public int CourseID { get; set; }
    public Course Course { get; set; } = null!;

    public int StudentID { get; set; }
    public Student Student { get; set; } = null!;
}
}