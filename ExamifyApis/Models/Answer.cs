using Microsoft.Data.SqlClient.DataClassification;

namespace ExamifyApis.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AnswerOption { get; set; } = "";
        public bool IsCorrect { get; set; }
        public double Grade { get; set; }

    }
}
