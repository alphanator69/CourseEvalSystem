using System;

namespace CourseEvaluationSystem.Models
{
    public class Evaluation
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }

        public int StudentID { get; set; }
    }
}
