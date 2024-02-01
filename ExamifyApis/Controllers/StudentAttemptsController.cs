using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamifyApis.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttemptsController : ControllerBase
    {
        private readonly StudentAttemptsServices services;
        public StudentAttemptsController(StudentAttemptsServices services)
        {
            this.services = services;
        }
        [HttpGet("GetOrCreateStudentAttempts/{studentId}/{examId}")]
        public async Task<ResponseClass<StudentAttempts>> GetOrCreateStudentAttempts(int studentId, int examId)
        {
            var studentAttempts = await services.GetOrCreateStudentAttempts(studentId, examId);
            return studentAttempts;
        }
        [HttpPost("AddAttempt/{studentAttemptsId}")]
        public async Task<ResponseClass<Attempt>> AddAttempt(int studentAttemptsId)
        {
            var studentAttempts = await services.AddAttempt(studentAttemptsId);
            return studentAttempts;
        }

        [HttpGet("GetAttemptsId/{studentId}/{examId}")]
        public async Task<ResponseClass<List<int>>> GetAttemptsId(int studentId, int examId)
        {
            var studentAttempts = await services.GetAttemptsId(studentId, examId);
            return studentAttempts;
        }

        [HttpGet("GetAttempt/{id}")]
        public async Task<ResponseClass<Attempt>> GetAttempt(int id)
        {
            var studentAttempts = await services.GetAttempt(id);
            return studentAttempts;
        }
    }
}
