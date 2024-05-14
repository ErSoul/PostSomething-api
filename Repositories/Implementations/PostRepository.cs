using Microsoft.EntityFrameworkCore;
using PostSomething_api.Database;
using PostSomething_api.Models;
using PostSomething_api.Repositories.Interface;
using PostSomething_api.Requests;
using System.Linq.Expressions;

namespace PostSomething_api.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context, ILogger<Repository<Post>> _logger) : base(context, _logger) { }

        public async Task<Post> CreatePost(PostRequest post)
        {
            var dbPost = new Post()
            {
                Title = post.Title,
                Description = post.Body,
                Image = post.Image,
                AuthorId = post.Author!
            };

            return await CreateAsync(dbPost);
        }

        public override Task<Post?> Find(Expression<Func<Post, bool>> predicate)
        {
            return _context.Posts.Include(p => p.Author).Where(predicate).FirstOrDefaultAsync();
        }
    }
}