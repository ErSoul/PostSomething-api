using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PostSomething_api.Models;

namespace PostSomething_api.Database
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<ApiUser>(options)
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasOne(comment => comment.Author).WithMany(user => user.Comments).OnDelete(DeleteBehavior.NoAction);
        }
    }
}