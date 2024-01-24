namespace ExamifyApis.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public string Grade { get; set; } = string.Empty;
        public ICollection<Course>? Courses { get; set; } = null;
        public List<StudentAttempts>? StudentAttempts { get; set; } = null;
    }
}
