using System.ComponentModel.DataAnnotations;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostInteractionRequest
    {
        [Required]
        public required Guid UserProfileId { get; set; }
        
        [Required]
        public required InteractionType InteractionType { get; set; }
    }
}
