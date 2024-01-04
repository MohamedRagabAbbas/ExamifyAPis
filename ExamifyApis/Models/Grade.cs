namespace ExamifyApis.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal TotalGrade { get; set; }


    }
}
