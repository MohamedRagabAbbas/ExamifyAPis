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
                .Include(sa => sa.Attempts).Select(
                sa => new StudentAttempts
                {
                    Id = sa.Id,
                    StudentId = sa.StudentId,
                    ExamId = sa.ExamId,
                    Attempts = sa.Attempts.Select(a => new Attempt
                    {
                        Id = a.Id,
                    }).ToList()
                })
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

        public async Task<ResponseClass<List<int>>> GetAttemptsId(int studentId, int examId)
        {
            try
            {
                var studentAttempts = await _context.StudentAttempts
                    .Include(sa => sa.Attempts)
                    .Where(sa => sa.StudentId == studentId && sa.ExamId == examId)
                    .Select(sa => new StudentAttempts
                    {
                        Id = sa.Id,
                        Attempts = sa.Attempts
                            .Select(a => new Attempt
                            {
                                Id = a.Id,
                            }).ToList()
                    })
                    .FirstOrDefaultAsync();
                if (studentAttempts == null)
                {
                    return new ResponseClass<List<int>>
                    {
                        Status = false,
                        Message = "Student Attempts Not Found",
                        Data = null
                    };
                }
                var attemptsId = studentAttempts.Attempts.Select(a => a.Id).ToList();
                return new ResponseClass<List<int>>
                {
                    Status = true,
                    Message = "Student Attempts Found",
                    Data = attemptsId
                };
            }
            catch (Exception e)
            {
                return new ResponseClass<List<int>>
                {
                    Status = false,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseClass<Attempt>> GetAttempt(int attemptId)
        {
            try
            {
                var attempt = await _context.Attempts.Include(x=>x.Answers)
                .Select(a=> new Attempt
                {
                    Id = a.Id,
                    Answers = a.Answers.Select(x=> new Answer
                    {
                        Id = x.Id,
                        Question = x.Question,
                        AnswerOption = x.AnswerOption,
                        IsCorrect = x.IsCorrect,
                        Grade = x.Grade
                    }).ToList(),
                    Grade = a.Grade
                }
                )
                .FirstOrDefaultAsync(x=> x.Id == attemptId);
                if (attempt == null)
                {
                    return new ResponseClass<Attempt>
                    {
                        Status = false,
                        Message = "Attempt Is Not Found",
                        Data = null
                    };
                }
                return new ResponseClass<Attempt>
                {
                    Status = true,
                    Message = "Attempt Found",
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
