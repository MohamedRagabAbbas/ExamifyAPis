using ExamifyApis.Models;

namespace ExamifyApis.ModelServices
{
    public class AnswerInfo
    {
        public int QuestionId { get; set; }
        public int AttemptId { get; set; }
        public string AnswerOption { get; set; } = "";
        public bool IsCorrect { get; set; }
        public double Grade { get; set; }
    }
}
