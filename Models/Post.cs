using System.Diagnostics.CodeAnalysis;

namespace PostSomething_api.Models
{
    public class Post
    {
        public int Id { get; set; }
#pragma warning disable CS8618
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public ApiUser Author { get; set; }
#pragma warning restore CS8618
        public string Image { get; set; } = "img/default.png";
        [AllowNull]
        public ICollection<Comment> Comments { get; set; }
    }
}
