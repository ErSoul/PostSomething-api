using PostSomething_api.Models;
using PostSomething_api.Requests;

namespace PostSomething_api.Services.Interface
{
    public interface ICommentsService
    {
        Task<Comment> CreateCommentFromPost(int postId, CommentRequest commentRequest, string userId);
        Task<IEnumerable<Comment>> GetCommentsFromPost(int postId);
        Task<Comment?> GetComment(int id);
        Task Delete(int id);
    }
}
