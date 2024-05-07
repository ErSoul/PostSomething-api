namespace PostSomething_api.Requests
{
    public class PostRequest
    {
#pragma warning disable CS8618
        public string Title { get; set; }
        public string Body { get; set; }
#pragma warning restore CS8618
        public string? Image { get; set; }
        public string? Author { get; set; }
    }
}
