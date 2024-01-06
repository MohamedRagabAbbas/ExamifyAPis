using ExamifyApis.ModelServices;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamifyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        // Add a constructor to inject the Teacher Service
        private readonly TeacherServices _teacherServices;
        public TeacherController(TeacherServices teacherServices)
        {
            _teacherServices = teacherServices;
        }
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var response = _teacherServices.GetAllTeachers();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacher(int id)
        {
            var response = _teacherServices.GetTeacher(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody] TeacherInfo teacherInfo)
        {
            var response = _teacherServices.AddTeacher(teacherInfo);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateTeacher([FromBody] TeacherInfo teacherInfo)
        {
            var response = _teacherServices.UpdateTeacher(teacherInfo);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveTeacherFromCourse(int id)
        {
            var response = _teacherServices.RemoveTeacherFromCourse(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveTeacher(int id)
        {
            var response = _teacherServices.DeleteTeacher(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddCourseToTeacher(int teacherId, int courseId)
        {
            var response = _teacherServices.AddTeacherToCourse(teacherId, courseId);
            return Ok(response);
        }


    }
}
