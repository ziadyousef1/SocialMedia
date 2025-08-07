using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostCommentCreateRequest
    {
        [Required]
        public required string Text { get; set; }
        
        [Required]
        public required Guid UserProfileId { get; set; }
    }
}
