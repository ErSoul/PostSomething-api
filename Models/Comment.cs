using System.Diagnostics.CodeAnalysis;

namespace PostSomething_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
#pragma warning disable CS8618
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public ApiUser Author { get; set; }
        public Post Post { get; set; }
#pragma warning restore CS8618
        public int? ParentId { get; set; }
        [AllowNull]
        public Comment Parent { get; set; }
        [AllowNull]
        public ICollection<Comment> Comments { get; set; }
    }
}
