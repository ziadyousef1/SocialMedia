using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostUpdateRequest
    {
        [Required]
        public required string TextContent { get; set; }
        
        public IFormFile? Photo { get; set; }
        
        public bool RemovePhoto { get; set; } = false;
    }
}
