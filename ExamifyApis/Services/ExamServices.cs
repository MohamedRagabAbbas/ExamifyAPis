using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class ExamServices
    {
        private readonly DBContextClass _dbContext;
        private readonly UserManager<ApplicationUser> _user;
        public ExamServices(DBContextClass dbContext, UserManager<ApplicationUser> user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        // section one
        // Basic Exam Operations
        public async Task<ResponseClass<List<Exam>>> GetAllExams()
        {
            var exams = await _dbContext.Exams.ToListAsync();
            if (exams != null)
            {
                ResponseClass<List<Exam>> response = new ResponseClass<List<Exam>>()
                {
                    Data = exams,
                    Message = "All Exams are send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Exam>> response = new ResponseClass<List<Exam>>()
                {
                    Message = "No Exams are Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Exam>> AddExam(ExamInfo examInfo)
        {
            if (examInfo != null)
            {
                Exam exam = new Exam()
                {
                    Name = examInfo.Name,
                    Description = examInfo.Description,
                    StartTime = examInfo.StartTime,
                    EndTime = examInfo.EndTime,
                    CourseId = examInfo.CourseId,
                    AttemptsNumber = examInfo.AttemptsNumber,
                    StudentAttempts = new List<StudentAttempts>(),
                    Duration = examInfo.Duration
                };
                await _dbContext.Exams.AddAsync(exam);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Exam is added successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Exam is not added...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Exam>> GetExam(int id)
        {
            var exam = await _dbContext.Exams.FindAsync(id);
            if (exam != null)
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Exam is send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Exam is not Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Exam>> UpdateExam(int id, ExamInfo examInfo)
        {
            var exam = await _dbContext.Exams.FindAsync(id);
            if (exam != null)
            {
                exam.Name = examInfo.Name;
                exam.Description = examInfo.Description;
                exam.StartTime = examInfo.StartTime;
                exam.EndTime = examInfo.EndTime;
                exam.UpdatedOn = DateTime.Now;
                exam.CourseId = examInfo.CourseId;
                exam.AttemptsNumber = examInfo.AttemptsNumber;
                exam.Duration = examInfo.Duration;
                await _dbContext.SaveChangesAsync();
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Exam is updated successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Exam is not updated...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Exam>> DeleteExam(int id)
        {
            var exam = await _dbContext.Exams.FindAsync(id);
            if (exam != null)
            {
                _dbContext.Exams.Remove(exam);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Exam is deleted successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Exam is not deleted...",
                };
                return response;
            }
        }

        // section two
        // add course to exam

        public async Task<ResponseClass<Exam>> AddCourseToExam(int examId, int courseId)
        {
            var exam = await _dbContext.Exams.FindAsync(examId);
            var course = await _dbContext.Courses.FindAsync(courseId);
            if (exam != null && course != null)
            {
                exam.CourseId = courseId;
                await _dbContext.SaveChangesAsync();
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Course is added to Exam successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Course is not added to Exam...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Exam>> RemoveCourseFromExam(int examId, int courseId)
        {
            var exam = await _dbContext.Exams.FindAsync(examId);
            var course = await _dbContext.Courses.FindAsync(courseId);
            if (exam != null && course != null)
            {
                exam.CourseId = 0;
                await _dbContext.SaveChangesAsync();
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Data = exam,
                    Message = "Course is removed from Exam successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Exam> response = new ResponseClass<Exam>()
                {
                    Message = "Course is not removed from Exam...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<List<Exam>>> GetExamsByCourse(int courseId)
        {
            var exams = await _dbContext.Exams.Where(e => e.CourseId == courseId).ToListAsync();
            if (exams != null)
            {
                ResponseClass<List<Exam>> response = new ResponseClass<List<Exam>>()
                {
                    Data = exams,
                    Message = "All Exams are send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Exam>> response = new ResponseClass<List<Exam>>()
                {
                    Message = "No Exams are Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<string>> GetTeacherName(int examId)
        {
            var exam = await _dbContext.Exams.FindAsync(examId);
            if(exam is not null)
            {
                var course = await _dbContext.Courses.FindAsync(exam.CourseId);
                if(course is not null)
                {
                    var teacher = await _dbContext.Teachers.FindAsync(course.TeacherId);
                    if(teacher is not null)
                    {
                       var user = _user.FindByIdAsync(teacher.ApplicationUserId).Result;
                        if (user is not null)
                        {
                            ResponseClass<string> response3 = new ResponseClass<string>()
                            {
                                Data = user.UserName,
                                Message = "Teacher Name is send successfully...",
                                Status = true
                            };
                            return response3;
                        }
                        else
                        {
                            ResponseClass<string> response2 = new ResponseClass<string>()
                            {
                                Message = "Teacher Name is not Found...",
                            };
                            return response2;
                        }
                    }
                    ResponseClass<string> response = new ResponseClass<string>()
                    {
                        Message = "Teacher is not Found...",
                    };
                    return response;

                }
                else
                {
                    ResponseClass<string> response = new ResponseClass<string>()
                    {
                        Message = "Course is not Found...",
                    };
                    return response;
                }
            }
            else
            {
                ResponseClass<string> response = new ResponseClass<string>()
                {
                    Message = "Exam is not Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<double>> GetTotalMark(int examId)
        {
            var exam = await _dbContext.Exams.FindAsync(examId);
            if (exam != null)
            {
                var questions = await _dbContext.Questions.Where(q => q.ExamId == examId).ToListAsync();
                if (questions != null)
                {
                    double totalMark = 0;
                    foreach (var question in questions)
                    {
                        totalMark += question.Weight;
                    }
                    ResponseClass<double> response = new ResponseClass<double>()
                    {
                        Data = totalMark,
                        Message = "Total Mark is send successfully...",
                        Status = true
                    };
                    return response;
                }
                else
                {
                    ResponseClass<double> response = new ResponseClass<double>()
                    {
                        Message = "No Questions are Found...",
                    };
                    return response;
                }
            }
            else
            {
                ResponseClass<double> response = new ResponseClass<double>()
                {
                    Message = "Exam is not Found...",
                };
                return response;
            }
        }

    }
}
