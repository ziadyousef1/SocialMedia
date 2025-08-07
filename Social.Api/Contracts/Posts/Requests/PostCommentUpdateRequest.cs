using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostCommentUpdateRequest
    {
        [Required]
        public required string Text { get; set; }
    }
}
