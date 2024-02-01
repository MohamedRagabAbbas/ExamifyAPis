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
    public class GradeController : ControllerBase
    {
        // Add a constructor to inject the Grade Service
        private readonly GradeServices _gradeServices;
        public GradeController(GradeServices gradeServices)
        {
            _gradeServices = gradeServices;
        }
        [HttpGet("GetAllGrades")]
        public async Task<ResponseClass<List<Grade>>> GetAllGrades()
        {
            var response = await _gradeServices.GetGrades();
            return response;
        }

        [HttpPost("AddGrade")]
        public async Task<ResponseClass<Grade>> AddGrade([FromBody] GradeInfo gradeInfo)
        {
            var response = await _gradeServices.AddGrade(gradeInfo);
            return response;
        }

    }
}
