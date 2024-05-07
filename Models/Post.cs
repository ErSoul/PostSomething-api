using System.Diagnostics.CodeAnalysis;

namespace PostSomething_api.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public ApiUser Author { get; set; } = null!;
        public string? Image { get; set; } = "img/default.png";
        [AllowNull]
        public ICollection<Comment> Comments { get; set; }
    }
}