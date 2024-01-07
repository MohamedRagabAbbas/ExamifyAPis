using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
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
        [HttpGet("GetAllCourses")]
        public async Task<ResponseClass<List<Course>>> GetAllCourses()
        {
            var response = await _courseServices.GetCourses();
            return response;
        }

        [HttpGet("GetCourse/{id}")]
        public async Task<ResponseClass<Course>> GetCourse(int id)
        {
            var response = await _courseServices.GetCourse(id);
            return response;
        }

        [HttpPost("AddCourse")]
        public async Task<ResponseClass<Course>> AddCourse([FromBody] CourseInfo courseInfo)
        {
            var response = await _courseServices.AddCourse(courseInfo);
            return response;
        }

        [HttpPut("UpdateCourse/{id}")]
        public async Task<ResponseClass<Course>> UpdateCourse(int id,[FromBody] CourseInfo courseInfo)
        {
            var response = await _courseServices.UpdateCourse(id,courseInfo);
            return response;
        }

        [HttpDelete("RemoveCourse/{id}")]
        public async Task<ResponseClass<Course>> RemoveCourse(int id)
        {
            var response = await _courseServices.DeleteCourse(id);
            return response;
        }

    }

}
