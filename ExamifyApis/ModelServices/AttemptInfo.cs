using ExamifyApis.Models;

namespace ExamifyApis.ModelServices
{
    public class AttemptInfo
    {
        public int StudentAttemptsId { get; set; }
        public StudentAttempts StudentAttempts { get; set; } = null!;
    }
}
