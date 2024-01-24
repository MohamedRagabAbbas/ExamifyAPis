namespace ExamifyApis.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = "";
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int CourseId { get; set; }
        public List<Course>? Courses { get; set;}
    }
}
