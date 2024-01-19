using Microsoft.Data.SqlClient.DataClassification;

namespace ExamifyApis.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AttemptId { get; set; }
        public Attempt Attempt { get; set; }
        public string AnswerOption { get; set; } = "";
        public bool IsCorrect { get; set; }
        public double Grade { get; set; }

    }
}
