using ExamifyApis.ModelServices;
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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionServices service;
        public QuestionController(QuestionServices _service)
        {
            service = _service;
        }
        [HttpPost("AddQuestion")]
        public async Task<IActionResult> AddQuestion([FromBody]QuestionInfo questionInfo)
        {
            var response = await service.AddQuestion(questionInfo);
            return Ok(response);
        }
        [HttpPut("UpdateQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody]QuestionInfo questionInfo)
        {
            var response = await service.UpdateQuestion(id, questionInfo);
            return Ok(response);
        }
        [HttpDelete("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var response = await service.DeleteQuestion(id);
            return Ok(response);
        }
        [HttpGet("GetQuestionsByExamId/{id}")]

        public async Task<IActionResult> GetQuestionsByExamId(int id)
        {
            var response = await service.GetQuestionsByExamId(id);
            return Ok(response);
        }

        [HttpGet("GetQuestionById/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var response = await service.GgetQuestion(id);
            return Ok(response);
        }


       
    }
}
