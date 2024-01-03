namespace ExamifyApis.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;

        public ICollection<Student>? Students { get; set; } = null;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set;}

        public List<Exam> Exams { get; set; }
    }
}
