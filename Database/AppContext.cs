using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostSomething_auth.Models;

namespace PostSomething_auth.Database
{
    public class AppContext : IdentityDbContext<ApiUser>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
