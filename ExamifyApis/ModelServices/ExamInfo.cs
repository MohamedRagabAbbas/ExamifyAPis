﻿namespace ExamifyApis.ModelServices
{
    public class ExamInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AttemptsNumber { get; set; } = 1;
        public int CourseId { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public int Duration { get; set; } = 60;

    }
}
