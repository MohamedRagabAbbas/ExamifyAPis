using ExamifyApis.Models;

namespace ExamifyApis.ModelServices
{
    public class GradeInfo
    {
        public int AttemptId { get; set; }
        public decimal TotalGrade { get; set; }
        public decimal OutOf { get; set; }
    }
}
