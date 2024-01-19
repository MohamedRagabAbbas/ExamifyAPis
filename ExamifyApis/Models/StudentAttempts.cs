namespace ExamifyApis.Models
{
    public class StudentAttempts
    {
        public int Id { get; set; }
        public List<Attempt>? Attempts { get; set; } = null!;
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
