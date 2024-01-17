namespace ExamifyApis.ModelServices
{
    public class QuestionInfo
    {
        public string QuestionNumber { get; set; } = "";
        public string QuestionText { get; set; } = "";
        public string Option1 { get; set; } = "";
        public string Option2 { get; set; } = "";
        public string Option3 { get; set; } = "";
        public string Option4 { get; set; } = "";
        public string CorrectAnswer { get; set; } = "";
        public double Weight { get; set; } = 0;
        public int ExamId { get; set; }
    }
}
