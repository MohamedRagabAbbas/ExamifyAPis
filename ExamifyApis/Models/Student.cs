namespace ExamifyApis.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Course>? Courses { get; set; } = null;
        public List<Answer>? Answers { get; set; } = null;
        public List<Grade>? Grades { get; set; }
    }
}
