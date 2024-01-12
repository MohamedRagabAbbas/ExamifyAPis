using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExamifyApis.Services
{
    public class UserServices
    {
        private readonly DBContextClass dBContext;
        public UserServices(DBContextClass _dBContext)
        {
            this.dBContext = _dBContext;
        }

        public async Task<ResponseClass<User>> AddUser(User user)
        {
            var response = await dBContext.Users.AddAsync(user);
            if(response!=null)
            {
                await dBContext.SaveChangesAsync();
                return new ResponseClass<User>()
                {
                    Status = true,
                    Message = "User Added Successfully",
                    Data = user
                };
            }
            else
            {
                return new ResponseClass<User>()
                {
                    Status = false,
                    Message = "Something went Wrong",
                    Data = user
                };
            }
        }
        public async Task<ResponseClass<User>> GetUser(int id)
        {
            var response = await dBContext.Users.FindAsync(id);
            if(response!=null)
            {
                return new ResponseClass<User>()
                {
                    Status = true,
                    Message = "User Found",
                    Data = response
                };
            }
            else
            {
                return new ResponseClass<User>()
                {
                    Status = false,
                    Message = "User Not Found",
                    Data = response
                };
            }
        }

        public async Task<ResponseClass<List<User>>> GetAllUsers()
        {
            var response = await dBContext.Users.ToListAsync();
            if(response!=null)
            {
                return new ResponseClass<List<User>>()
                {
                    Status = true,
                    Message = "Users Found",
                    Data = response
                };
            }
            else
            {
                return new ResponseClass<List<User>>()
                {
                    Status = false,
                    Message = "Users Not Found",
                    Data = response
                };
            }
        }

        public async Task<ResponseClass<User>> UpdateUser(int id, User user)
        {
            var response = await dBContext.Users.FindAsync(id);
            if(response!=null)
            {
                response.UserEmail = user.UserEmail;
                response.UserPassword = user.UserPassword;
                response.UserRole = user.UserRole;
                response.Status = user.Status;
                await dBContext.SaveChangesAsync();
                return new ResponseClass<User>()
                {
                    Status = true,
                    Message = "User Updated Successfully",
                    Data = response
                };
            }
            else
            {
                return new ResponseClass<User>()
                {
                    Status = false,
                    Message = "User Not Found",
                    Data = response
                };
            }
        }

        public async Task<ResponseClass<User>> RemoveUser(int id)
        {
            var response = await dBContext.Users.FindAsync(id);
            if(response!=null)
            {
                dBContext.Users.Remove(response);
                await dBContext.SaveChangesAsync();
                return new ResponseClass<User>()
                {
                    Status = true,
                    Message = "User Removed Successfully",
                    Data = response
                };
            }
            else
            {
                return new ResponseClass<User>()
                {
                    Status = false,
                    Message = "User Not Found",
                    Data = response
                };
            }

        }
        public Task<ResponseClass<User>> ChangeStatus(int id, bool status)
        {
            var user = dBContext.Users.Find(id);
            if(user!=null)
            {
                user.Status = status;
                dBContext.SaveChanges();
                return Task.FromResult(new ResponseClass<User>()
                {
                    Status = true,
                    Message = "Status Changed Successfully",
                    Data = user
                });
            }
            else
            {
                return Task.FromResult(new ResponseClass<User>()
                {
                    Status = false,
                    Message = "User Not Found",
                    Data = user
                });
            }
        }

    }
}
