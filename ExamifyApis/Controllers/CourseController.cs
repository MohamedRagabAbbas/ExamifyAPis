using ExamifyApis.ModelServices;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamifyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // Add a constructor to inject the Course Service
        private readonly CourseServices _courseServices;
        public CourseController(CourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var response = _courseServices.GetCourses();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var response = _courseServices.GetCourse(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseInfo courseInfo)
        {
            var response = _courseServices.AddCourse(courseInfo);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id,[FromBody] CourseInfo courseInfo)
        {
            var response = _courseServices.UpdateCourse(id,courseInfo);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCourse(int id)
        {
            var response = _courseServices.DeleteCourse(id);
            return Ok(response);
        }

    }

}
