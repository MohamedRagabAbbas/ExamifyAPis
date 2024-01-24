using ExamifyApis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamifyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthSignUpController : ControllerBase
    {
        private readonly AuthenticationManagement _authenticationManagement;

        public AuthSignUpController(AuthenticationManagement authenticationManagement)
        {
            _authenticationManagement = authenticationManagement;
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] ExamifyApis.ModelServices.AuthSignUp model)
        {
            var response = await _authenticationManagement.Register(model);
            if(response is null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] ExamifyApis.ModelServices.AuthLogIn model)
        {
            var response = await _authenticationManagement.LogIn(model);
            if(response is null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
