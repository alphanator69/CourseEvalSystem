using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseEvaluationSystem.Models
{

    public class Student
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}

