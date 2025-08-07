namespace Social.Api.Contracts.Posts.Responses
{
    public class PostCommentResponse
    {
        public Guid PostCommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
