namespace ExamifyApis.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Ques { get; set; } = "";
        public string Option1 { get; set; } = "";
        public string Option2 { get; set; } = "";
        public string Option3 { get; set; } = "";
        public string Option4 { get; set; } = "";
        public string CorrectAnswer { get; set; } = "";
        public double Weight { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
