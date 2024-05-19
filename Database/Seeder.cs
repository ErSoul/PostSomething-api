using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostSomething_api.Models;

namespace PostSomething_api.Database
{
    public static class Seeder
    {
        public static async Task Run(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApiUser>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            await UserSeeder(userManager);
            await PostsSeeder(dbContext);
        }

        private static async Task UserSeeder(UserManager<ApiUser> userManager)
        {
            var defaultUser = await userManager.Users.Where(u => u.UserName == "admin@example.org").SingleOrDefaultAsync();

            if (defaultUser == null)
            {
                defaultUser = new ApiUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user@example.org",
                    EmailConfirmed = true,
                    UserName = "user",
                    NormalizedUserName = "USER",
                    NormalizedEmail = "USER@EXAMPLE.ORG",
                };

                await userManager.CreateAsync(defaultUser, "P@ssw0rd");
            }
        }

        private static async Task PostsSeeder(ApplicationContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Posts.Any())
                return;

            var post = new Post
            {
                Author = dbContext.Users.First(),
                AuthorId = dbContext.Users.First().Id,
                Title = "Lorem Ipsum noblekr iadsfoqwpdsao fjdsaonom qoiewf",
                Description = "Loremp idspfajsdiuqfoiwe qdsk alfnqewiofj qiurwoe fnqioqf qw. fiqwoefj iowfjsadifl mq3ref.qf qrwioj fwiqofj kdsalfm qoiweiq jfriwgteiouqhg ioqjfqwkl mdfsa."
            };

            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
        }
    }
}
