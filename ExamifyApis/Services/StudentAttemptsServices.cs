using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class StudentAttemptsServices
    {
        private readonly DBContextClass _context;
        public StudentAttemptsServices(DBContextClass context)
        {
            _context = context;
        }
        
        public async Task<ResponseClass<StudentAttempts>> GetOrCreateStudentAttempts(int studentId, int examId)
        {
            var studentAttempts = await _context.StudentAttempts
                .Include(sa => sa.Attempts)
                .FirstOrDefaultAsync(sa => sa.StudentId == studentId && sa.ExamId == examId);
            try {                 
                if (studentAttempts == null)
                    {
                        studentAttempts = new StudentAttempts
                        {
                            StudentId = studentId,
                            ExamId = examId,
                            Attempts = new List<Attempt>()
                        };
                        await _context.StudentAttempts.AddAsync(studentAttempts);
                        await _context.SaveChangesAsync();
                    }
                return new ResponseClass<StudentAttempts>
                {
                    Status = true,
                    Message = "Student Attempts Found",
                    Data = studentAttempts
                };
            }
            catch (Exception e)
            {
                return new ResponseClass<StudentAttempts>
                {
                    Status = false,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseClass<Attempt>> AddAttempt(int studentAttemptsId)
        {
            try
            {
                Attempt attempt = new Attempt
                {
                    StudentAttemptsId = studentAttemptsId
                };
                await _context.Attempts.AddAsync(attempt);
                await _context.SaveChangesAsync();
                return new ResponseClass<Attempt>
                {
                    Status = true,
                    Message = "Attempt Added",
                    Data = attempt
                };
            }
            catch (Exception e)
            {
                return new ResponseClass<Attempt>
                {
                    Status = false,
                    Message = e.Message,
                    Data = null
                };
            }
        }
        
    }
}
