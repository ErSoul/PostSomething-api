using Microsoft.AspNetCore.Identity;

namespace PostSomething_auth.Models
{
    public class ApiUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
