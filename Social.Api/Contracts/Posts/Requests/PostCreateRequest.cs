using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostCreateRequest
    {
        [Required]
        public required string TextContent { get; set; }
        
        [Required]
        public required Guid UserProfileId { get; set; }
        
        public IFormFile? Photo { get; set; }
    }
}
