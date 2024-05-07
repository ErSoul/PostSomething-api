using PostSomething_api.Models;
using PostSomething_api.Repositories.Interface;
using PostSomething_api.Requests;
using PostSomething_api.Services.Interface;

namespace PostSomething_api.Services.Implementation
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserManager<ApiUser> _userManager;
        public CommentsService(ICommentRepository commentRepository, IPostRepository postRepository, IUserManager<ApiUser> userManager)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Comment>> GetCommentsFromPost(int postId)
        {
            return await _commentRepository.GetCommentsFromPost(postId);
        }

        public Task<Comment?> GetComment(int id)
        {
            return _commentRepository.GetComment(id);
        }

        public async Task<Comment> CreateCommentFromPost(int postId, CommentRequest commentRequest, string userId)
        {
            var post = await _postRepository.Find(post => post.Id == postId);
            var user = await _userManager.FindByIdAsync(userId);
            Comment? parentComment = await _commentRepository.Find(comment => comment.Id == commentRequest.CommentId);

            if (post == null)
                throw new ArgumentNullException(nameof(postId));

            if (parentComment is not null && parentComment.Post.Id != post.Id)
                throw new BadHttpRequestException("Bad Request");

            var comment = await _commentRepository.CreateComment(commentRequest, user!, post, parentComment);
            return comment;
        }

        public async Task Delete(int id)
        {
            var comment = await _commentRepository.Find(comment => comment.Id == id) ?? throw new ArgumentNullException(nameof(id));
            await _commentRepository.Delete(comment);
        }
    }
}