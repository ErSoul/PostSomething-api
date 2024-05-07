namespace PostSomething_api.Requests
{
    public class CommentRequest
    {
        public required string Content { get; set; }
        // Parent Comment ID
        public int? CommentId { get; set; }
    }
}