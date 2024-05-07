namespace PostSomething_api.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public Comment? Parent { get; set; }
        public Post Post { get; set; }
        public Comment(Models.Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            Author = new User(comment.Author);
            Post = new Post(comment.Post);
            Parent = comment.Parent is not null ? new Comment(comment.Parent) : null;
        }
    }
}