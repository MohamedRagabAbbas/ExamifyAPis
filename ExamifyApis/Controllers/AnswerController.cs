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
    public class AnswerController : ControllerBase
    {
        // Add a constructor to inject the Answer Service
        private readonly AnswerServices _answerServices;
        public AnswerController(AnswerServices answerServices)
        {
            _answerServices = answerServices;
        }
        [HttpGet("GetAllAnswers")]
        public async Task<ResponseClass<List<Answer>>> GetAllAnswers()
        {
            var response = await _answerServices.GetAnswers();
            return response;
        }

        [HttpGet("GetAnswer/{id}")]
        public async Task<ResponseClass<Answer>> GetAnswer(int id)
        {
            var response = await _answerServices.GetAnswerById(id);
            return response;
        }

        [HttpPost("AddAnswer")]
        public async Task<ResponseClass<Answer>> AddAnswer([FromBody] AnswerInfo answerInfo)
        {
            var response = await _answerServices.AddAnswer(answerInfo);
            return response;
        }

        [HttpPut("UpdateAnswer/{id}")]
        public async Task<ResponseClass<Answer>> UpdateAnswer(int id, [FromBody] AnswerInfo answerInfo)
        {
            var response = await _answerServices.UpdateAnswer(id, answerInfo);
            return response;
        }

        [HttpDelete("RemoveAnswer/{id}")]
        public async Task<ResponseClass<Answer>> RemoveAnswer(int id)
        {
            var response = await _answerServices.DeleteAnswer(id);
            return response;
        }

    }
}
