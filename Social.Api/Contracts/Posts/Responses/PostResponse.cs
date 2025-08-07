namespace Social.Api.Contracts.Posts.Responses
{
    public class PostResponse
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public required string TextContent { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int CommentCount { get; set; }
        public int InteractionCount { get; set; }
    }
}
