using System;

namespace CourseEvaluationSystem.Models
{
    public class Evaluation
    {
        public int ID { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
