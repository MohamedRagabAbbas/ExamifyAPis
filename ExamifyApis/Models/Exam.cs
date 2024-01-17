namespace ExamifyApis.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;

        public List<Question>? Questions { get; set; } 
        public int CourseId { get; set; } 
        public Course Course { get; set;} 
        public List<Grade>? Grades { get; set; }
        public List<Answer>? Answers { get; set; } = null;
    }
}
