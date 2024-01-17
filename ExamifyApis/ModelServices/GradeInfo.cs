using ExamifyApis.Models;

namespace ExamifyApis.ModelServices
{
    public class GradeInfo
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public decimal TotalGrade { get; set; }
    }
}
