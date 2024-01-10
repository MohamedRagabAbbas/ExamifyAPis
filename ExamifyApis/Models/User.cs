namespace ExamifyApis.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = "";
        public string UserPassword { get; set; } = "";
        public string UserRole { get; set; } = "";
        public bool Status { get; set; } = false;
    }
}
