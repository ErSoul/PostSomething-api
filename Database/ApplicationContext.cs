using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostSomething_api.Models;

namespace PostSomething_api.Database
{
    public class ApplicationContext : IdentityDbContext<ApiUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
