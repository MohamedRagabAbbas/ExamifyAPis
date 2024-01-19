using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamifyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // Add a constructor to inject the Stdeunt Service
        private readonly StudentServices _studentServices;
        public StudentController(StudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet("GetAllStudents")]
        public async Task<ResponseClass<List<Student>>>  GetAllStudents()
        {
            var response = await _studentServices.GetAllStudents();
            return response;
        }

        // GET api/<StudentController>/5
        [HttpGet("GetStudent/{id}")]
        public async Task<ResponseClass<Student>> GetStudent(int id)
        {
            var response = await _studentServices.GetStudent(id);
            return response;
        }

        [HttpPost("AddStudent")]
        public async Task<ResponseClass<Student>> AddStudent([FromBody] StudentInfo studentInfo)
        {
            var response = await _studentServices.AddStudent(studentInfo);
            return response;
        }

        [HttpPut("UpdateStudent/{id}")]
        public async Task<ResponseClass<Student>> UpdateStudent(int id,[FromBody] StudentInfo studentInfo)
        {
            var response = await _studentServices.UpdateStudent(id, studentInfo);
            return response;
        }

        [HttpDelete("RemoveStudent/{id}")]
        public async Task<ResponseClass<Student>> RemoveStudent(int id)
        {
            var response = await _studentServices.RemoveStudent(id);
            return response;
        }

        [HttpPost("AddCourseToStudent/{studentId}/{courseCode}")]
        public async Task<ResponseClass<Student>> AddCourseToStudent(int studentId, string courseCode)
        {
            var response = await _studentServices.AddCourseToStudent(studentId, courseCode);
            return response;
        }

        [HttpGet("GetAllCoursesForStudent/{studentId}")]
        public async Task<ResponseClass<ICollection<Course>>> GetAllCoursesForStudent(int studentId)
        {
            var response = await _studentServices.GetAllCoursesByStudentId(studentId);
            return response;
        }

        [HttpPut("UpdateCourseForStudent")] 
        public async Task<ResponseClass<Student>> UpdateCourseForStudent(int studentId, int courseId)
        {
            var response = await _studentServices.UpdateCourseForStudent(studentId, courseId);
            return response;
        }

        [HttpDelete("RemoveCourseFromStudent")] 
        public async Task<ResponseClass<Student>> RemoveCourseFromStudent(int studentId, int courseId)
        {
            var response = await _studentServices.RemoveCourseFromStudent(studentId, courseId);
            return response;
        }


        [HttpGet("Login/{email}/{password}")]
        public async Task<ResponseClass<Student>> Login(string email, string password)
        {
            var response = await _studentServices.Login(email, password);
            return response;
        }
    }
}
