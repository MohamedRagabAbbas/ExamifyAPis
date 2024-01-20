namespace ExamifyApis.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int AttemptId { get; set; }
        public Attempt Attempt { get; set; } = null!;
        public decimal TotalGrade { get; set; }
        public decimal OutOf { get; set; }

    }
}
