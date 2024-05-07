using PostEntity = PostSomething_api.Models.Post;

namespace PostSomething_api.DTO
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public Post(PostEntity postEntity)
        {
            Id = postEntity.Id;
            Title = postEntity.Title;
            Description = postEntity.Description;
            Author = new User(postEntity.Author);
        }
    }
}
