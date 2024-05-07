using PostSomething_api.Models;
using PostSomething_api.Requests;

namespace PostSomething_api.Repositories.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> CreateComment(CommentRequest comment, ApiUser userId, Post post, Comment? parent);
        Task<IEnumerable<Comment>> GetCommentsFromPost(int postId);
        Task<Comment?> GetComment(int id);
    }
}
