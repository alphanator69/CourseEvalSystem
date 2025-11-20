using System.Collections.Generic;

namespace CourseEvaluationSystem.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
