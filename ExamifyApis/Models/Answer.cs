using Microsoft.Data.SqlClient.DataClassification;

namespace ExamifyApis.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerOption { get; set; } = "";
        public bool IsCorrect { get; set; }
        public double Grade { get; set; };
    }
}
