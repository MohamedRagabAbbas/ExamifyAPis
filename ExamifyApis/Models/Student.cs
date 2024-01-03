﻿namespace ExamifyApis.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public List<Course>? Courses { get; set; } = null;
    }
}
