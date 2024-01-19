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
        [HttpPost("AddAttempt")]
        public async Task<ResponseClass<Attempt>> AddAttempt(int studentAttemptsId)
        {
            var studentAttempts = await services.AddAttempt(studentAttemptsId);
            return studentAttempts;
        }
    }
}
