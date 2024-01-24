using Microsoft.AspNetCore.Identity;

namespace ExamifyApis.Models
{
    public class ApplicationUser: IdentityUser
    {   
        public Student? Student { get; set; } = null!;
        public Teacher? Teacher { get; set; } = null!;

    }
}
