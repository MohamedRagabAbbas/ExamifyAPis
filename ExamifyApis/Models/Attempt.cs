namespace ExamifyApis.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public List<Answer>? Answers { get; set; }
        public Grade? Grade { get; set; } = null!;
        public int StudentAttemptsId { get; set; }
        public StudentAttempts StudentAttempts { get; set; } = null!;
    }
}
