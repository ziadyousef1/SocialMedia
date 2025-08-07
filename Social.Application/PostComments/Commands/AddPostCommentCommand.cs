using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.Commands
{
    public class AddPostCommentCommand : IRequest<OperationResult<PostComment>>
    {
        public required Guid PostId { get; set; }
        public required Guid UserProfileId { get; set; }
        public required string Text { get; set; }
    }
}
