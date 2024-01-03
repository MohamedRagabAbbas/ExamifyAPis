namespace ExamifyApis.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public double TotalGrade { get; set; }
    }
}
