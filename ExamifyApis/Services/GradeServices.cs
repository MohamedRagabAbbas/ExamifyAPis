using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class GradeServices
    {
        private readonly DBContextClass _context;
        public GradeServices(DBContextClass context)
        {
            _context = context;
        }
        public async Task<ResponseClass<List<Grade>>> GetGrades()
        {
            var grades = await _context.Grades.ToListAsync();
            if(grades!=null)
            {
                ResponseClass<List<Grade>> response = new ResponseClass<List<Grade>>()
                {
                    Data = grades,
                    Message = "All Grades are send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Grade>> response = new ResponseClass<List<Grade>>()
                {
                    Message = "No Grades are Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Grade>> AddGrade(GradeInfo gradeInfo)
        {
            if(gradeInfo!=null)
            {
                Grade grade = new Grade()
                {
                    AttemptId = gradeInfo.AttemptId,
                    TotalGrade = gradeInfo.TotalGrade
                };
                await _context.Grades.AddAsync(grade);
                await _context.SaveChangesAsync();
                ResponseClass<Grade> response = new ResponseClass<Grade>()
                {
                    Data = grade,
                    Message = "Grade is added successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Grade> response = new ResponseClass<Grade>()
                {
                    Message = "Grade is not added...",
                };
                return response;
            }
        }

    }
}
