namespace PostSomething_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public ApiUser Author { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}