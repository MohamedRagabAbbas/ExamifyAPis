using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.DB;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class CourseServices
    {
        private readonly DBContextClass _context;
        public CourseServices(DBContextClass context)
        {
            _context = context;
        }
        public async Task<ResponseClass<List<Course>>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            if(courses!=null)
            {
                ResponseClass<List<Course>> response = new ResponseClass<List<Course>>()
                {
                    Data = courses,
                    Message = "All Courses are send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<List<Course>> response = new ResponseClass<List<Course>>()
                {
                    Message = "No Courses are Found...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Course>> AddCourse(CourseInfo courseInfo, int teacherId)
        {
            if(courseInfo!=null)
            {
                Course course = new Course()
                {
                    Code = courseInfo.Code,
                    Subject = courseInfo.Subject,
                    Grade = courseInfo.Grade,
                    TeacherId = teacherId
                };
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Data = course,
                    Message = "Course is added successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = "Course is not added...",
                };
                return response;
            }
        }
        public async Task<ResponseClass<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course!=null)
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Data = course,
                    Message = "Course is send successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = "Course is not found...",
                };
                return response;
            }
        }
        public async Task<ResponseClass<Course>> UpdateCourse(int id, CourseInfo courseInfo)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course!=null)
            {
                course.Code = courseInfo.Code;
                course.Subject = courseInfo.Subject;
                course.Grade = courseInfo.Grade;
                await _context.SaveChangesAsync();
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Data = course,
                    Message = "Course is updated successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = "Course is not updated...",
                };
                return response;
            }
        }
        public async Task<ResponseClass<Course>> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course!=null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Data = course,
                    Message = "Course is deleted successfully...",
                    Status = true
                };
                return response;
            }
            else
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = "Course is not deleted...",
                };
                return response;
            }
        }

        public async Task<ResponseClass<List<Exam>>> GetExamsByCourseId(int courseId)
        {
            var exams = await _context.Exams.Where(e=>e.CourseId==courseId).ToListAsync();
            if(exams!=null)
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


    }
}
