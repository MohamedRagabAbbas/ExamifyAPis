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
    public class ExamController : ControllerBase
    {
        // Add a constructor to inject the Exam Service
        private readonly ExamServices _examServices;
        public ExamController(ExamServices examServices)
        {
            _examServices = examServices;
        }
        [HttpGet("GetAllExams")]
        public async Task<ResponseClass<List<Exam>>> GetAllExams()
        {
            var response = await _examServices.GetAllExams();
            return response;
        }

        [HttpGet("GetExam/{id}")]
        public async Task<ResponseClass<Exam>> GetExam(int id)
        {
            var response = await _examServices.GetExam(id);
            return response;
        }

        [HttpPost("AddExam")]
        public async Task<ResponseClass<Exam>> AddExam([FromBody] ExamInfo examInfo)
        {
            var response = await _examServices.AddExam(examInfo);
            return response;
        }

        [HttpPut("UpdateExam/{id}")]
        public async Task<ResponseClass<Exam>> UpdateExam(int id, [FromBody] ExamInfo examInfo)
        {
            var response = await _examServices.UpdateExam(id,examInfo);
            return response;
        }

        [HttpDelete("RemoveExam/{id}")]
        public async Task<ResponseClass<Exam>> RemoveExam(int id)
        {
            var response = await _examServices.DeleteExam(id);
            return response;
        }



    }
}
