using MediatR;
using Social.Application.Models;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Application.Posts.Commands
{
    public class UpdatePostCommand : IRequest<OperationResult<PostEntity>>
    {
        public required Guid PostId { get; set; }
        public required string TextContent { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoFileName { get; set; }
        public bool RemovePhoto { get; set; } = false;
    }
}
