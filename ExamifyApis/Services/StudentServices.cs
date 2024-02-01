using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.Response;
using ExamifyApis.ModelServices;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ExamifyApis.Services
{
    public class StudentServices
    {
        private readonly DBContextClass _context;
        public StudentServices(DBContextClass context) 
        { 
            _context = context;
            
        }


        // Section 1 
        // Basic operations on Student, Operations like Add, Remove, Update, Get, GetAll

        public async Task<ResponseClass<Student>> AddStudent(StudentInfo studentInfo)
        {
            Student student = new Student() { 
                Grade = studentInfo.Grade,
                ApplicationUserId = studentInfo.ApplicationUserId,
                Courses = new List<Course>(),
                StudentAttempts = new List<StudentAttempts>(),
                };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            //create a response object
            ResponseClass<Student> response = new ResponseClass<Student>() {
                Message = "Student Added Successfully",
                Status = true,
                Data = student
              };
            return response;

        }
        public async Task<ResponseClass<Student>> RemoveStudent(int Student_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            if(student!=null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Student Removed Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Unsuccessful Process, Student Is Not Found",
                };
                return response;
            }
        }

        public async Task<ResponseClass<Student>> UpdateStudent(int id, StudentInfo studentInfo)
        {
            Student? student = await _context.Students.FindAsync(id);
            if(student!=null)
            {
                student.Grade = studentInfo.Grade;
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Student Updated Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Unsuccessful Process, Student Is Not Found",
                };
                return response;
            }
        }   

        public async Task<ResponseClass<Student>> GetStudent(int Student_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            if(student!=null)
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Student Found Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Unsuccessful Process, Student Is Not Found",
                };
                return response;
            }
        }
        public async Task<ResponseClass<List<Student>>> GetAllStudents()
        {
            List<Student>? students = await _context.Students.ToListAsync<Student>();
            if(students!=null)
            {
                ResponseClass<List<Student>> response = new ResponseClass<List<Student>>()
                {
                    Message = "Students Found Successfully",
                    Status = true,
                    Data = students
                };
                return response;
            }
            else
            {
                ResponseClass<List<Student>> response = new ResponseClass<List<Student>>()
                {
                    Message = "Unsuccessful Process, Students Are Not Found",
                };
                return response;
            }
        }

        // Section 2
        // Operations on Student's Courses, Operations like Add, Remove, Update, Get, GetAll

        public async Task<ResponseClass<Student>> AddCourseToStudent(int Student_Id, string courseCode)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Course? course = await _context.Courses.Where(x=> x.Code == courseCode).FirstAsync();
            if(student!=null && course!=null)
            {
                // if it is the first time to add a course to the student, even I did this when adding new student
                if(student.Courses==null)
                {
                    student.Courses = new List<Course>();
                }
                
                student.Courses.Add(course);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Course Added To Student Successfully",
                    Status = true,
                    Data = null
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Course Is Not Found"
                };
                return response;
            }
        }
        public async Task<ResponseClass<ICollection<Course>>> GetAllCoursesByStudentId(int id)
        {
            Student? student = await _context.Students.Include(s => s.Courses).Select(c => new Student
            {
                Id = c.Id,
                Grade = c.Grade,
                Courses = c.Courses.Select(c => new Course
                {
                    Id = c.Id,
                    Code = c.Code,
                    Subject = c.Subject,
                    Grade = c.Grade,
                    // Exclude Lists
                }).ToList(),
                // Exclude Lists
            }).FirstOrDefaultAsync(s => s.Id == id);

            if (student != null)
            {
                ICollection<Course>? courses = student.Courses;
                if(courses!=null)
                {
                    return new ResponseClass<ICollection<Course>>()
                    {
                        Message = "Courses Found Successfully",
                        Status = true,
                        Data = courses
                    };
                }
                else
                {
                    return new ResponseClass<ICollection<Course>>()
                    {
                        Message = "Unsuccessful Process, Courses Are Not Found",
                    };
                }

            }
            else
            {
                return new ResponseClass<ICollection<Course>>()
                {
                    Message = "Unsuccessful Process, Stdeunt Are Not Found",
                };
            }
        }


        public async Task<ResponseClass<Student>> RemoveCourseFromStudent(int Student_Id, int Course_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Course? course = await _context.Courses.FindAsync(Course_Id);
            if(student!=null && course!=null)
            {
                student.Courses.Remove(course);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Course Removed From Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Course Is Not Found"
                };
                return response;
            }
        } 
        
        public async Task<ResponseClass<Student>> UpdateCourseForStudent(int Student_Id, int Course_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Course? course = await _context.Courses.FindAsync(Course_Id);
            if(student!=null && course!=null)
            {
                student.Courses.Add(course);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Course Updated For Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Course Is Not Found"
                };
                return response;
            }
        }
        
        public async Task<ResponseClass<Course>> GetCourseForStudent(int Student_Id, int Course_Id)
        {
            Student? student = await  _context.Students.FindAsync(Student_Id);
            Course? course = await _context.Courses.FindAsync(Course_Id);

            if(student!=null&& course!=null)
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = "Course Found For Student Successfully",
                    Status = true,
                    Data = course
                };
                return response;
            }
            else
            {
                ResponseClass<Course> response = new ResponseClass<Course>()
                {
                    Message = student == null? $"This Student With Id = {Student_Id} Is Not Found": $"Course Is Not Found For This Student With Id = {Student_Id}",
                };
                return response;

            }
        }
        public async Task<ResponseClass<Student>> RemoveCourseForStudent(int Student_Id, int Course_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Course? course = await _context.Courses.FindAsync(Course_Id);
            if(student!=null && course!=null)
            {
                student.Courses.Remove(course);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Course Removed From Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Course Is Not Found"
                };
                return response;
            }
        }   

        public async Task<ResponseClass<int>> GetStudentId(string ApplicationUserId)
        {
            var student = await _context.Students.Where(x => x.ApplicationUserId == ApplicationUserId).FirstOrDefaultAsync();
            if(student is null)
            {
                return new ResponseClass<int>()
                {
                    Message = "Cannot Find This Student!",
                    Status = false,
                    
                };
            }
            return new ResponseClass<int>()
            {
                Message = "This Student Is Found Successfully...",
                Status = true,
                Data = student.Id
            };
        }

        //login

        /*public async Task<ResponseClass<Student>> Login(string Email, string Password)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.Email == Email && s.Password == Password);
            if(student!=null)
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Login Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Login Failed, Email Or Password Is Incorrect",
                };
                return response;
            }
        }*/

    }
}
