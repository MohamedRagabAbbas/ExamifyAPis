namespace ExamifyApis.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Question>? Questions { get; set; } 
        public int CourseId { get; set; } 
        public Course Course { get; set;} 
        public List<Grade>? Grades { get; set; }
        public List<Answer>? Answers { get; set; } = null;
    }
}
