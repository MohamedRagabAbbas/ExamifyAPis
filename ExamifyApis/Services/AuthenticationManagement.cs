using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExamifyApis.Services
{
    public class AuthenticationManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DBContextClass _dbContext;
        private readonly StudentServices _studentServices;
        private readonly TeacherServices _teacherServices;

        public AuthenticationManagement(UserManager<ApplicationUser> userManager, DBContextClass dbContext, StudentServices studentServices, TeacherServices teacherServices)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _studentServices = studentServices;
            _teacherServices = teacherServices;
        }

        public async Task<AuthenticationResponse> Register(AuthSignUp model)
        {
            if( await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthenticationResponse
                {
                    Message = "User with this email already exists",
                };
            }
            if(model.Role == "Student")
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    //add role to user
                    await _userManager.AddToRoleAsync(user, model.Role);
                    var student = new StudentInfo
                    {
                        Grade = model.Grade,
                        ApplicationUserId = await _userManager.GetUserIdAsync(user),
                    };
                    var retunedStudent = await _studentServices.AddStudent(student);
                    if (retunedStudent is null)
                    {
                        return new AuthenticationResponse
                        {
                            Message = "Student could not be created",
                        };
                    }
                    // add token to user using GenerateJwtToken function 
                    var token = GenerateJwtToken(user);
                    return new AuthenticationResponse
                    {
                        Message = "User is added successfully",
                        Role = model.Role,
                        Token = token,
                        IsAuthenticated = true,
                        Id = user.Id,
                        Name = user.UserName,
                    };
                }
                return new AuthenticationResponse
                {
                    Message = "Cannot Add This User, " + getErrors(result),
                    Role = model.Role,

                };
            }
            else if (model.Role == "Teacher")
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //add role to user
                    await _userManager.AddToRoleAsync(user, model.Role);
                    var teacher = new TeacherInfo
                    {
                        ApplicationUserId = await _userManager.GetUserIdAsync(user),
                    };
                    var retunedTeacher = await _teacherServices.AddTeacher(teacher);
                    if (retunedTeacher is null)
                    {
                        return new AuthenticationResponse
                        {
                            Message = "Teacher could not be created",
                        };
                    }
                    // add token to user using GenerateJwtToken function 
                    var token = GenerateJwtToken(user);
                    return new AuthenticationResponse
                    {
                        Message = "User is added successfully",
                        Role = model.Role,
                        Token = token,
                        IsAuthenticated = true,
                        Id = user.Id,
                        Name = user.UserName,
                    };
                }
                return new AuthenticationResponse
                {
                    Message = $"Cannot Create New User,{result.Errors} ",
                    Role = model.Role,

                };
            }
            else
            {
                return new AuthenticationResponse
                {
                    Message = "Cannot Add This User! " + model.Role,
                    Role = model.Role,
                };
            }
        }

        public async Task<AuthenticationResponse> LogIn(AuthLogIn model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user,model.Password))
            {
                return new AuthenticationResponse
                {
                    Message = "Error, Either Email Or Password is Incorrect!",
                };
            }
            var token = GenerateJwtToken(user);
            return new AuthenticationResponse
            {
                Message = "User Logged In Successfully",
                Role = (await _userManager.GetRolesAsync(user))[0],
                Token = token,
                IsAuthenticated = true,
                Id = user.Id,
                Name = user.UserName,
            };
        }

        // generate jwt token for user
        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string getErrors(IdentityResult Result)
        {
            string Error = string.Empty;
            foreach (var error in  Result.Errors)
            {
                Error += error.Code + " ==> " + error.Description + " , ";
            }
            return Error;
        }











    }
}
