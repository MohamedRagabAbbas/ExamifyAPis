using ExamifyApis.Models;
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
    public class UserController : ControllerBase
    {
        // Add a constructor to inject the User Service
        private readonly UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ResponseClass<List<User>>> GetAllUsers()
        {
            var response = await _userServices.GetAllUsers();
            return response;
        }

        // GET api/<UserController>/5
        [HttpGet("GetUser/{id}")]
        public async Task<ResponseClass<User>> GetUser(int id)
        {
            var response = await _userServices.GetUser(id);
            return response;
        }

        [HttpPost("AddUser")]
        public async Task<ResponseClass<User>> AddUser([FromBody] User user)
        {
            var response = await _userServices.AddUser(user);
            return response;
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ResponseClass<User>> UpdateUser(int id, [FromBody] User user)
        {
            var response = await _userServices.UpdateUser(id, user);
            return response;
        }

        [HttpDelete("RemoveUser/{id}")]
        public async Task<ResponseClass<User>> RemoveUser(int id)
        {
            var response = await _userServices.RemoveUser(id);
            return response;
        }

        [HttpGet("ChangeStatus")]
        public async Task<ResponseClass<User>> ChangeStatus(int id,bool status)
        {
            var response = await _userServices.ChangeStatus(id, status);
            return response;
        }
    }
}
