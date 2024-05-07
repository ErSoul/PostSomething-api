using Microsoft.EntityFrameworkCore;

using PostSomething_api.Database;
using PostSomething_api.Models;
using PostSomething_api.Repositories.Interface;
using PostSomething_api.Requests;

namespace PostSomething_api.Repositories.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context, ILogger<Repository<Comment>> _logger) : base(context, _logger)
        {
        }

        public async Task<Comment> CreateComment(CommentRequest commentRequest, ApiUser user, Post post, Comment? parent)
        {
            var comment = new Comment
            {
                Post = post,
                Content = commentRequest.Content,
                Author = user,
                Parent = parent
            };

            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsFromPost(int postId)
        {
            return await _context.Comments.Include(comment => comment.Author).Where(comments => comments.Post.Id == postId && comments.ParentId == null).ToListAsync();
        }

        public Task<Comment?> GetComment(int id)
        {
            return _context.Comments.Include(comment => comment.Author).Include(comment => comment.Post).FirstOrDefaultAsync(comment => comment.Id == id);
        }
    }
}