using PostSomething_api.Models;
using PostSomething_api.Requests;

namespace PostSomething_api.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> CreatePost(PostRequest post);
    }
}