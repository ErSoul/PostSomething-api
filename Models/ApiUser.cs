using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace PostSomething_api.Models
{
    public class ApiUser : IdentityUser
    {
        public string? Address { get; set; }
        [AllowNull]
        public ICollection<Post> Posts { get; set; }
        [AllowNull]
        public ICollection<Comment> Comments { get; set; }
    }
}