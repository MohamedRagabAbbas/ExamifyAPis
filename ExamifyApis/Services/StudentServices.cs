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
                Email = studentInfo.Email,
                Grade = studentInfo.Grade,
                Name = studentInfo.Name,
                Password = studentInfo.Password,
                Answers = new List<Answer>(),
                Courses = new List<Course>(),
                Grades = new List<Grade>()
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
                student.Name = studentInfo.Name;
                student.Grade = studentInfo.Grade;
                student.Email = studentInfo.Email;
                student.Password = studentInfo.Password;
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
                Name = c.Name,
                Email = c.Email,
                Grade = c.Grade,
                Password = c.Password,
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

        // Section 4
        // Operations on Student's Grades, Operations like Add, Remove, Update, Get, GetAll
        public async Task<ResponseClass<Student>> AddGradeToStudent(int Student_Id, int Grade_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Grade? grade = await _context.Grades.FindAsync(Grade_Id);
            if(student!=null && grade!=null)
            {
                // if it is the first time to add a grade to the student, even I did this when adding new student
                if(student.Grades==null)
                {
                    student.Grades = new List<Grade>();
                }
                
                student.Grades.Add(grade);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Grade Added To Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Grade Is Not Found"
                };
                return response;
            }
        }   

        public async Task<ResponseClass<Student>> RemoveGradeFromStudent(int Student_Id, int Grade_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Grade? grade = await _context.Grades.FindAsync(Grade_Id);
            if(student!=null && grade!=null)
            {
                student.Grades.Remove(grade);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Grade Removed From Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Grade Is Not Found"
                };
                return response;
            }
        }

        public async Task<ResponseClass<Student>> UpdateGradeForStudent(int Student_Id, int Grade_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Grade? grade = await _context.Grades.FindAsync(Grade_Id);
            if(student!=null && grade!=null)
            {
                student.Grades.Add(grade);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Grade Updated For Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Grade Is Not Found"
                };
                return response;
            }
        }

        public async Task<ResponseClass<Grade>> GetGradeForStudent(int Student_Id, int Grade_Id)
        {
            Student? student = await  _context.Students.FindAsync(Student_Id);
            Grade? grade = await _context.Grades.FindAsync(Grade_Id);

            if(student!=null&& grade!=null)
            {
                ResponseClass<Grade> response = new ResponseClass<Grade>()
                {
                    Message = "Grade Found For Student Successfully",
                    Status = true,
                    Data = grade
                };
                return response;
            }
            else
            {
                ResponseClass<Grade> response = new ResponseClass<Grade>()
                {
                    Message = student == null? $"This Student With Id = {Student_Id} Is Not Found": $"Grade Is Not Found For This Student With Id = {Student_Id}",
                };
                return response;

            }
        }

        //section 5 
        // oprations of student's asnwers on questions, operations like add, remove, update, get, getall
        public async Task<ResponseClass<Student>> AddAnswerToStudent(int Student_Id, int Answer_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Answer? answer = await _context.Answers.FindAsync(Answer_Id);
            if(student!=null && answer!=null)
            {
                // if it is the first time to add an answer to the student, even I did this when adding new student
                if(student.Answers==null)
                {
                    student.Answers = new List<Answer>();
                }
                
                student.Answers.Add(answer);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Answer Added To Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Answer Is Not Found"
                };
                return response;
            }
        }

        public async Task<ResponseClass<Student>> RemoveAnswerFromStudent(int Student_Id, int Answer_Id)
        {
            Student? student = await _context.Students.FindAsync(Student_Id);
            Answer? answer = await _context.Answers.FindAsync(Answer_Id);
            if(student!=null && answer!=null)
            {
                student.Answers.Remove(answer);
                _context.SaveChanges();
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = "Answer Removed From Student Successfully",
                    Status = true,
                    Data = student
                };
                return response;
            }
            else
            {
                ResponseClass<Student> response = new ResponseClass<Student>()
                {
                    Message = student==null? "Unsuccessful Process, Student Is Not Found": "Unsuccessful Process, Answer Is Not Found"
                };
                return response;
            }
        } 
        
        //login

        public async Task<ResponseClass<Student>> Login(string Email, string Password)
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
        }

    }
}
