using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.Response;
using ExamifyApis.ModelServices;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class TeacherServices
    {
        private readonly DBContextClass _dbContext;
        public TeacherServices(DBContextClass dbContext)
        {
            _dbContext = dbContext;
            
        }
        //section 1 
        // Basic Oppeartions for teacher
        public async Task<ResponseClass<Teacher>> AddTeacher(TeacherInfo teacherInfo)
        {
            if(teacherInfo != null)
            {
                Teacher teacher = new Teacher()
                {
                    ApplicationUserId = teacherInfo.ApplicationUserId,
                };
                await _dbContext.Teachers.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                   Data = teacher,
                   Message = "Teacher  is added successfully",
                   Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not added",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<Teacher>> GetTeacher(int id)
        {
            Teacher teacher = await _dbContext.Teachers.FindAsync(id);
            if(teacher != null)
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not found",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<Teacher>> UpdateTeacher(int id, TeacherInfo teacherInfo)
        {
            Teacher teacher = await _dbContext.Teachers.FindAsync(id);
            if(teacher != null)
            {
                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is updated successfully",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not updated",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<Teacher>> DeleteTeacher(int id)
        {
            Teacher teacher = await _dbContext.Teachers.FindAsync(id);
            if(teacher != null)
            {
                _dbContext.Teachers.Remove(teacher);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is deleted successfully",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not deleted",
                    Status = false
                };
                return response;
            }
        }
        //section 2
        // Advance operations for teacher
        public async Task<ResponseClass<List<Teacher>>> GetAllTeachers()
        {
            List<Teacher> teachers = await _dbContext.Teachers.ToListAsync();
            if(teachers != null)
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = teachers,
                    Message = "Teachers are found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = null,
                    Message = "Teachers are not found",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<List<Teacher>>> GetTeachersByCourseId(int courseId)
        {
            List<Teacher> teachers = await _dbContext.Teachers.Where(t => t.CourseId == courseId).ToListAsync();
            if(teachers != null)
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = teachers,
                    Message = "Teachers are found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = null,
                    Message = "Teachers are not found",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<List<Teacher>>> GetTeachersByCourseName(string courseName)
        {
            List<Teacher> teachers = await _dbContext.Teachers.Where(t => t.Courses.Any(c => c.Subject == courseName)).ToListAsync();
            if(teachers != null)
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = teachers,
                    Message = "Teachers are found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = null,
                    Message = "Teachers are not found",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<List<Teacher>>> GetTeachersByCourseCode(string courseCode)
        {
            List<Teacher> teachers = await _dbContext.Teachers.Where(t => t.Courses.Any(c => c.Code == courseCode)).ToListAsync();
            if(teachers != null)
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = teachers,
                    Message = "Teachers are found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Teacher>> response = new ResponseClass<List<Teacher>>()
                {
                    Data = null,
                    Message = "Teachers are not found",
                    Status = false
                };
                return response;
            }
        }
        // add teacher to course
        public async Task<ResponseClass<Teacher>> AddTeacherToCourse(int teacherId, int courseId)
        {
            Teacher teacher = await _dbContext.Teachers.FindAsync(teacherId);
            Course course = await _dbContext.Courses.FindAsync(courseId);
            if(teacher != null && course != null)
            {
                teacher.CourseId = courseId;
                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is added to course successfully",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not added to course",
                    Status = false
                };
                return response;
            }
        }

        // remove teacher from course
        public async Task<ResponseClass<Teacher>> RemoveTeacherFromCourse(int teacherId)
        {
            Teacher teacher = await _dbContext.Teachers.FindAsync(teacherId);
            if(teacher != null)
            {
                teacher.CourseId = 0;
                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync();
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is removed from course successfully",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not removed from course",
                    Status = false
                };
                return response;
            }

        }

        // login

       /* public async Task<ResponseClass<Teacher>> Login(string email, string password)
        {
            Teacher teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Email == email && t.Password == password);
            if(teacher != null)
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = teacher,
                    Message = "Teacher is logged in successfully",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Teacher> response = new ResponseClass<Teacher>()
                {
                    Data = null,
                    Message = "Teacher is not logged in",
                    Status = false
                };
                return response;
            }
        } */

        // get all courses for teacher with id = id
        public async Task<ResponseClass<List<Course>>> GetCoursesByTeacherId(int id)
        {
            Teacher? teacher = await _dbContext.Teachers.FindAsync(id);
            if(teacher != null)
            {
                List<Course> courses = await _dbContext.Courses
                .Where(c => c.TeacherId == id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Code = c.Code,
                    Subject = c.Subject,
                    Grade = c.Grade,
                    Students = c.Students,
                    Exams = c.Exams,
                    // Exclude Teacher property
                })
                .ToListAsync();
                ResponseClass<List<Course>> response = new ResponseClass<List<Course>>
                {
                    Data = courses,
                    Message = "Courses are found",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Course>> response = new ResponseClass<List<Course>>
                {
                    Data = null,
                    Message = "Courses are not found",
                    Status = false
                };
                return response;
            }
        }
        public async Task<ResponseClass<int>> GetTeacherId(string ApplicationUserId)
        {
            var teacher = await _dbContext.Teachers.Where(x => x.ApplicationUserId == ApplicationUserId).FirstOrDefaultAsync();
            if (teacher is null)
            {
                return new ResponseClass<int>()
                {
                    Message = "Cannot Find This Teacher!",
                    Status = false,

                };
            }
            return new ResponseClass<int>()
            {
                Message = "This Teacher Is Found Successfully...",
                Status = true,
                Data = teacher.Id
            };
        }
    }
}
