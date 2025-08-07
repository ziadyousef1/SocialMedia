using MediatR;
using Social.Application.Models;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.Commands
{
    public class CreatePostCommand : IRequest<OperationResult<PostEntity>>
    {
        public required Guid UserProfileId { get; set; }
        public required string TextContent { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
