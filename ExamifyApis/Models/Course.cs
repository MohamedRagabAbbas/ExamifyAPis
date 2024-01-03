namespace ExamifyApis.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
    }
}
