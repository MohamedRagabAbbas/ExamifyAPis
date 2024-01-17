using ExamifyApis.Models;

namespace ExamifyApis.ModelServices
{
    public class AnswerInfo
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerOption { get; set; } = "";
        public bool IsCorrect { get; set; }
        public double Grade { get; set; }
    }
}
