using ExamifyApis.ModelServices;
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
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var response = _studentServices.GetAllStudents();
            return Ok(response);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var response = _studentServices.GetStudent(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentInfo studentInfo)
        {
            var response = _studentServices.AddStudent(studentInfo);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody] StudentInfo studentInfo)
        {
            var response = _studentServices.UpdateStudent(studentInfo);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveStudent(int id)
        {
            var response = _studentServices.RemoveStudent(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddCourseToStudent(int studentId, int courseId)
        {
            var response = _studentServices.AddCourseToStudent(studentId, courseId);
            return Ok(response);
        }

        [HttpPut] IActionResult UpdateCourseForStudent(int studentId, int courseId)
        {
            var response = _studentServices.UpdateCourseForStudent(studentId, courseId);
            return Ok(response);
        }

        [HttpDelete] IActionResult RemoveCourseFromStudent(int studentId, int courseId)
        {
            var response = _studentServices.RemoveCourseFromStudent(studentId, courseId);
            return Ok(response);
        }

        [HttpPost] IActionResult AddAnswerToStudent(int studentId, int answerId)
        {
            var response = _studentServices.AddAnswerToStudent(studentId, answerId);
            return Ok(response);
        }

        [HttpDelete]

        IActionResult RemoveAnswerFromStudent(int studentId, int answerId)
        {
            var response = _studentServices.RemoveAnswerFromStudent(studentId, answerId);
            return Ok(response);
        }

        [HttpPost] IActionResult AddGradeToStudent(int studentId, int gradeId)
        {
            var response = _studentServices.AddGradeToStudent(studentId, gradeId);
            return Ok(response);
        }
        [HttpDelete] IActionResult RemoveGradeFromStudent(int studentId, int gradeId)
        {
            var response = _studentServices.RemoveGradeFromStudent(studentId, gradeId);
            return Ok(response);
        }
        

    }
}
