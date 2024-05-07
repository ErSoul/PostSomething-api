using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;

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