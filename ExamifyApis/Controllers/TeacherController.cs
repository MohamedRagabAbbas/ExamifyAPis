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
    public class TeacherController : ControllerBase
    {
        // Add a constructor to inject the Teacher Service
        private readonly TeacherServices _teacherServices;
        public TeacherController(TeacherServices teacherServices)
        {
            _teacherServices = teacherServices;
        }
        [HttpGet("GetAllTeachers")]
        public async Task<ResponseClass<List<Teacher>>> GetAllTeachers()
        {
            var response = await _teacherServices.GetAllTeachers();
            return response;
        }

        [HttpGet("GetTeacher/{id}")]
        public async Task<ResponseClass<Teacher>> GetTeacher(int id)
        {
            var response = await _teacherServices.GetTeacher(id);
            return response;
        }

        [HttpPost("AddTeacher")]
        public async Task<ResponseClass<Teacher>> AddTeacher([FromBody] TeacherInfo teacherInfo)
        {
            var response = await _teacherServices.AddTeacher(teacherInfo);
            return response;
        }

        [HttpPut("UpdateTeacher/{id}")]
        public async Task<ResponseClass<Teacher>> UpdateTeacher(int id,[FromBody] TeacherInfo teacherInfo)
        {
            var response = await _teacherServices.UpdateTeacher(id,teacherInfo);
            return response;
        }

        [HttpDelete("RemoveTeacherFromCourse/{id}")]
        public async Task<ResponseClass<Teacher>> RemoveTeacherFromCourse(int id)
        {
            var response = await _teacherServices.RemoveTeacherFromCourse(id);
            return response;
        }

        [HttpDelete("RemoveTeacher/{id}")]
        public async Task<ResponseClass<Teacher>> RemoveTeacher(int id)
        {
            var response = await _teacherServices.DeleteTeacher(id);
            return response;
        }

        [HttpPost("AddCourseToTeacher")]
        public async  Task<ResponseClass<Teacher>> AddCourseToTeacher(int teacherId, int courseId)
        {
            var response = await _teacherServices.AddTeacherToCourse(teacherId, courseId);
            return response;
        }


    }
}
