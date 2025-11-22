using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseEvaluationSystem.Models
{
    public class Course
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public double AverageRating => Evaluations.Any() ? Evaluations.Average(e => e.Rating) : 0;
    }
}