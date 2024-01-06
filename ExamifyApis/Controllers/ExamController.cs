using ExamifyApis.ModelServices;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamifyApis.Controllers
{
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
        [HttpGet]
        public IActionResult GetAllExams()
        {
            var response = _examServices.GetAllExams();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetExam(int id)
        {
            var response = _examServices.GetExam(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddExam([FromBody] ExamInfo examInfo)
        {
            var response = _examServices.AddExam(examInfo);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExam(int id, [FromBody] ExamInfo examInfo)
        {
            var response = _examServices.UpdateExam(id,examInfo);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveExam(int id)
        {
            var response = _examServices.DeleteExam(id);
            return Ok(response);
        }

    }
}
