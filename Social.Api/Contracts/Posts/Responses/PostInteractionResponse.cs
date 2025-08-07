using Social.Domain.Aggregates.PostAggregate;

namespace Social.Api.Contracts.Posts.Responses
{
    public class PostInteractionResponse
    {
        public Guid PostInteractionId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public DateTime InteractionDate { get; set; }
        public InteractionType InteractionType { get; set; }
    }
}
